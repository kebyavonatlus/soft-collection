/**
 * Боковое меню — одно на всё приложение (ТЗ, раздел 5).
 * Ключ `tab` — строка матрицы доступов на Э-11 (Нет / Просмотр / Полный).
 */
export type TabKey =
  | 'myCases'
  | 'promises'
  | 'groupCases'
  | 'approvals'
  | 'legal'
  | 'dashboard'
  | 'reports'
  | 'strategies'
  | 'integrations'
  | 'users'
  | 'audit'

export type AccessLevel = 'none' | 'view' | 'full'

export interface NavItem {
  tab: TabKey
  screen: string
  path: string
  icon: string
  /** Этап плана, в котором экран появляется. */
  milestone: string
}

export interface NavSection {
  key: 'work' | 'analytics' | 'system'
  items: NavItem[]
}

export const navigation: NavSection[] = [
  {
    key: 'work',
    items: [
      { tab: 'myCases', screen: 'Э-02', path: '/my-cases', icon: 'pi pi-inbox', milestone: 'S2' },
      { tab: 'promises', screen: 'Э-02', path: '/promises', icon: 'pi pi-calendar', milestone: 'S2' },
      { tab: 'groupCases', screen: 'Э-04', path: '/group-cases', icon: 'pi pi-users', milestone: 'S4' },
      { tab: 'approvals', screen: 'Э-05', path: '/approvals', icon: 'pi pi-check-square', milestone: 'Этап 2' },
      { tab: 'legal', screen: 'Э-09', path: '/legal', icon: 'pi pi-briefcase', milestone: 'Этап 3' },
    ],
  },
  {
    key: 'analytics',
    items: [
      { tab: 'dashboard', screen: 'Э-06', path: '/dashboard', icon: 'pi pi-chart-bar', milestone: 'S4' },
      { tab: 'reports', screen: 'Э-07', path: '/reports', icon: 'pi pi-list', milestone: 'S5' },
    ],
  },
  {
    key: 'system',
    items: [
      { tab: 'strategies', screen: 'Э-08', path: '/strategies', icon: 'pi pi-sliders-h', milestone: 'S2' },
      { tab: 'integrations', screen: 'Э-10', path: '/integrations', icon: 'pi pi-link', milestone: 'S1' },
      { tab: 'users', screen: 'Э-11', path: '/users', icon: 'pi pi-user', milestone: 'S4' },
      { tab: 'audit', screen: 'Э-12', path: '/audit', icon: 'pi pi-file', milestone: 'S4' },
    ],
  },
]

/**
 * Меню для сотрудника: вкладка видна, если хотя бы одна его роль даёт к ней доступ;
 * раздел без видимых вкладок скрывается.
 */
export function visibleNavigation(access: Partial<Record<TabKey, AccessLevel>>): NavSection[] {
  return navigation
    .map((section) => ({
      ...section,
      items: section.items.filter((item) => (access[item.tab] ?? 'none') !== 'none'),
    }))
    .filter((section) => section.items.length > 0)
}

/** Доступы ролей складываются: по каждой вкладке берётся больший (ТЗ, раздел 2). */
export function mergeAccess(
  ...roles: Partial<Record<TabKey, AccessLevel>>[]
): Partial<Record<TabKey, AccessLevel>> {
  const rank: Record<AccessLevel, number> = { none: 0, view: 1, full: 2 }
  const result: Partial<Record<TabKey, AccessLevel>> = {}
  for (const role of roles) {
    for (const [tab, level] of Object.entries(role) as [TabKey, AccessLevel][]) {
      const current = result[tab] ?? 'none'
      if (rank[level] > rank[current]) result[tab] = level
    }
  }
  return result
}
