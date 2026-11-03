<script setup lang="ts">
import { computed } from 'vue'
import { useRoute } from 'vue-router'
import { useI18n } from 'vue-i18n'
import Tag from 'primevue/tag'

const route = useRoute()
const { t } = useI18n()
const title = computed(() => t(`nav.${String(route.meta.tab)}`))
</script>

<template>
  <header class="page-header">
    <p class="breadcrumb">{{ route.meta.screen }}</p>
    <h1>{{ title }}</h1>
  </header>
  <div class="card">
    <p>{{ t('placeholder.inProgress') }}</p>
    <Tag severity="secondary" :value="t('placeholder.plannedIn', { milestone: route.meta.milestone })" />
  </div>
</template>

<style scoped>
.breadcrumb {
  color: var(--sc-text-muted);
  font-size: 13px;
  margin: 0;
}
h1 {
  margin: 4px 0 24px;
  font-size: 28px;
}
.card {
  background: #fff;
  border: 1px solid var(--sc-border);
  border-radius: 10px;
  padding: 24px;
}
</style>
