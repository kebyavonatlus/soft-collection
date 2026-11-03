<script setup lang="ts">
import { RouterView } from 'vue-router'
import { useI18n } from 'vue-i18n'
import { ScUiProvider } from '@/shared/ui'
import type { AppLocale } from '@/i18n'

const { locale } = useI18n()
</script>

<template>
  <ScUiProvider :locale="locale as AppLocale">
    <RouterView />
  </ScUiProvider>
</template>
