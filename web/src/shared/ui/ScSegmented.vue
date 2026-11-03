<script setup lang="ts" generic="T extends string">
import { ElSegmented } from 'element-plus'

const model = defineModel<T>({ required: true })

defineProps<{
  options: { label: string; value: T }[]
  size?: 'small' | 'default'
}>()
</script>

<template>
  <ElSegmented v-model="model" :options="options" :size="size ?? 'default'" />
</template>
