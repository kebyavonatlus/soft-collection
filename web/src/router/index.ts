import { createRouter, createWebHistory, type RouteRecordRaw } from 'vue-router'
import AppLayout from '@/layouts/AppLayout.vue'
import { navigation } from '@/app/navigation'

declare module 'vue-router' {
  interface RouteMeta {
    tab?: string
    screen?: string
    milestone?: string
    public?: boolean
  }
}

// Пока каждый экран — заглушка; модули (modules/*) заменят их по мере реализации.
const screenRoutes: RouteRecordRaw[] = navigation.flatMap((section) =>
  section.items.map((item) => ({
    path: item.path.slice(1),
    name: item.tab,
    component: () => import('@/views/PlaceholderView.vue'),
    meta: { tab: item.tab, screen: item.screen, milestone: item.milestone },
  })),
)

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    { path: '/login', name: 'login', component: () => import('@/views/LoginView.vue'), meta: { public: true } },
    {
      path: '/',
      component: AppLayout,
      // Главный экран основной роли (ТЗ, Э-01); по умолчанию коллектор → Э-02.
      children: [{ path: '', redirect: '/my-cases' }, ...screenRoutes],
    },
    { path: '/:pathMatch(.*)*', name: 'not-found', component: () => import('@/views/NotFoundView.vue') },
  ],
})

// TODO(S0 «Вход Э-01», S4 «Матрица доступов»): guard по сессии и уровню доступа к вкладке.

export default router
