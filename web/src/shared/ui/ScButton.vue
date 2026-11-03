<script setup lang="ts">
import { computed } from 'vue'
import { ElButton, ElTooltip } from 'element-plus'

/**
 * Кнопка. На экране одна синяя кнопка — главное действие (variant="primary"),
 * остальные белые с рамкой (ТЗ, раздел 5). Временно недоступная кнопка видна
 * серой с причиной в подсказке — передайте disabledReason.
 */
const props = withDefaults(
  defineProps<{
    variant?: 'primary' | 'secondary' | 'danger' | 'link'
    nativeType?: 'button' | 'submit' | 'reset'
    block?: boolean
    loading?: boolean
    disabledReason?: string | null
  }>(),
  { variant: 'secondary', nativeType: 'button', block: false, loading: false, disabledReason: null },
)

defineEmits<{ click: [event: MouseEvent] }>()

const type = computed(() => (props.variant === 'primary' ? 'primary' : props.variant === 'danger' ? 'danger' : undefined))
const disabled = computed(() => !!props.disabledReason)
</script>

<template>
  <ElTooltip :disabled="!disabledReason" :content="disabledReason ?? ''" placement="top">
    <span class="sc-button-wrap" :class="{ block }">
      <ElButton
        :type="type"
        :link="variant === 'link'"
        :native-type="nativeType"
        :loading="loading"
        :disabled="disabled"
        :class="{ block }"
        size="large"
        @click="$emit('click', $event)"
      >
        <slot />
      </ElButton>
    </span>
  </ElTooltip>
</template>

<style scoped>
.sc-button-wrap {
  display: inline-flex;
}
.block,
.block :deep(.el-button) {
  display: flex;
  width: 100%;
}
</style>
