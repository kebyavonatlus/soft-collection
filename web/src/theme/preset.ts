import { definePreset } from '@primeuix/themes'
import Aura from '@primeuix/themes/aura'

/**
 * Пресет токенов под дизайн-систему «ЛБ Ломбард».
 * Цвета сняты с макетов (тёмно-синее меню, синяя главная кнопка) и должны быть
 * сверены с дизайнером — см. задачу «Пресет токенов ЛБ Ломбард».
 */
export const LbPreset = definePreset(Aura, {
  semantic: {
    primary: {
      50: '#eef3fa',
      100: '#d5e1f2',
      200: '#adc3e5',
      300: '#7f9fd3',
      400: '#4f77bb',
      500: '#2c5597',
      600: '#1f3f78',
      700: '#1a3463',
      800: '#16294d',
      900: '#13223f',
      950: '#0c1629',
    },
  },
})
