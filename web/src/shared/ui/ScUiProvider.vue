<script setup lang="ts">
import { computed } from 'vue'
import { ElConfigProvider } from 'element-plus'
import ru from 'element-plus/es/locale/lang/ru'
import ky from 'element-plus/es/locale/lang/ky'

/** Корневой провайдер UI-библиотеки: язык системных текстов (календари, пагинация). */
const props = defineProps<{ locale: 'ru' | 'ky' }>()
const elementLocale = computed(() => (props.locale === 'ky' ? ky : ru))
</script>

<template>
  <ElConfigProvider :locale="elementLocale">
    <slot />
  </ElConfigProvider>
</template>
