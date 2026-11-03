/**
 * Единственная точка доступа экранов к UI-библиотеке.
 * Element Plus импортируется только в src/shared/ui и src/theme — это проверяет ESLint
 * (правило no-restricted-imports в eslint.config.ts). Замена библиотеки затронет
 * только эти обёртки, а не экраны.
 */
export { default as ScButton } from './ScButton.vue'
export { default as ScInput } from './ScInput.vue'
export { default as ScSegmented } from './ScSegmented.vue'
export { default as ScTag } from './ScTag.vue'
export { default as ScUiProvider } from './ScUiProvider.vue'
