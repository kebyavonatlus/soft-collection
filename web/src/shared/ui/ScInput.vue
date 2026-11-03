<script setup lang="ts">
import { ElInput } from 'element-plus'

const model = defineModel<string>({ default: '' })

withDefaults(
  defineProps<{
    id?: string
    type?: 'text' | 'password' | 'search' | 'tel'
    placeholder?: string
    autocomplete?: string
    maxlength?: number
  }>(),
  { type: 'text' },
)
</script>

<template>
  <ElInput
    v-model="model"
    :id="id"
    :type="type === 'password' ? 'password' : 'text'"
    :show-password="type === 'password'"
    :placeholder="placeholder"
    :autocomplete="autocomplete"
    :maxlength="maxlength"
    size="large"
  />
</template>
