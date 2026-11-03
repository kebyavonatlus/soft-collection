<script setup lang="ts">
import { computed } from 'vue'
import { ElTag } from 'element-plus'

/** Статус-метка: «Просрочка 15 дн.», «Обещание сегодня», «Списан» и т. п. */
const props = withDefaults(defineProps<{ tone?: 'neutral' | 'info' | 'success' | 'warning' | 'danger' }>(), {
  tone: 'neutral',
})

const type = computed(() => (props.tone === 'neutral' ? 'info' : props.tone === 'info' ? 'primary' : props.tone))
</script>

<template>
  <ElTag :type="type" effect="light" disable-transitions>
    <slot />
  </ElTag>
</template>
