<script setup lang="ts">
import { computed } from 'vue'
import { RouterLink, RouterView } from 'vue-router'
import { useI18n } from 'vue-i18n'
import SelectButton from 'primevue/selectbutton'
import { navigation } from '@/app/navigation'
import { setLocale, type AppLocale } from '@/i18n'
import { formatDate } from '@/shared/format'

const { t, locale } = useI18n()

// Пока нет входа и матрицы доступов (S0/S4), показываем все вкладки —
// как после установки, когда все вкладки открыты всем ролям (ТЗ, раздел 2).
const sections = navigation

const languageOptions = [
  { label: 'Кыргызча', value: 'ky' },
  { label: 'Русский', value: 'ru' },
]
const currentLocale = computed({
  get: () => locale.value as AppLocale,
  set: (value: AppLocale) => value && setLocale(value),
})

// Опер. день придёт с бэкенда (дата последней успешной загрузки задолженности).
const operDay = formatDate(new Date())
</script>

<template>
  <div class="shell">
    <aside class="sidebar">
      <RouterLink to="/" class="brand">
        <span class="brand-logo">SC</span>
        <span>{{ t('app.name') }}</span>
      </RouterLink>

      <nav class="menu">
        <section v-for="section in sections" :key="section.key">
          <h2 class="menu-section">{{ t(`nav.${section.key}`) }}</h2>
          <RouterLink v-for="item in section.items" :key="item.tab" :to="item.path" class="menu-item">
            <i :class="item.icon" aria-hidden="true" />
            <span>{{ t(`nav.${item.tab}`) }}</span>
          </RouterLink>
        </section>
      </nav>

      <footer class="sidebar-footer">
        <div class="oper-day">
          <span>{{ t('layout.operDay') }}</span>
          <span class="mono">{{ operDay }}</span>
        </div>
        <SelectButton v-model="currentLocale" :options="languageOptions" option-label="label" option-value="value" size="small" :allow-empty="false" />
      </footer>
    </aside>

    <main class="content">
      <RouterView />
    </main>
  </div>
</template>

<style scoped>
.shell {
  display: grid;
  grid-template-columns: 240px 1fr;
  min-height: 100vh;
}
.sidebar {
  background: var(--sc-sidebar-bg);
  color: var(--sc-sidebar-fg);
  display: flex;
  flex-direction: column;
  padding: 20px 12px;
  gap: 16px;
}
.brand {
  display: flex;
  align-items: center;
  gap: 10px;
  color: #fff;
  font-weight: 600;
  font-size: 17px;
  text-decoration: none;
  padding: 0 12px;
}
.brand-logo {
  background: #fff;
  color: var(--sc-sidebar-bg);
  border-radius: 8px;
  width: 32px;
  height: 32px;
  display: grid;
  place-items: center;
  font-size: 12px;
  font-weight: 700;
}
.menu {
  flex: 1;
  display: flex;
  flex-direction: column;
  gap: 16px;
}
.menu-section {
  font-size: 12px;
  font-weight: 500;
  color: var(--sc-sidebar-muted);
  margin: 0 12px 6px;
}
.menu-item {
  display: flex;
  align-items: center;
  gap: 12px;
  padding: 9px 12px;
  border-radius: 8px;
  color: var(--sc-sidebar-fg);
  text-decoration: none;
  font-size: 14px;
}
.menu-item:hover {
  background: rgb(255 255 255 / 6%);
}
.menu-item.router-link-active {
  background: var(--sc-sidebar-active);
  color: #fff;
  font-weight: 600;
}
.sidebar-footer {
  display: flex;
  flex-direction: column;
  gap: 10px;
}
.oper-day {
  display: flex;
  justify-content: space-between;
  background: rgb(255 255 255 / 6%);
  border-radius: 6px;
  padding: 8px 12px;
  font-size: 12px;
}
.content {
  background: var(--sc-page-bg);
  padding: 32px 40px;
  min-width: 0;
}
@media (max-width: 1280px) {
  .shell {
    grid-template-columns: 72px 1fr;
  }
  .brand span:last-child,
  .menu-section,
  .menu-item span,
  .oper-day {
    display: none;
  }
}
</style>
