import { createI18n } from 'vue-i18n'
import ru from './locales/ru'
import ky from './locales/ky'

export type AppLocale = 'ru' | 'ky'

const STORAGE_KEY = 'sc.locale'

function initialLocale(): AppLocale {
  try {
    const saved = localStorage.getItem(STORAGE_KEY)
    if (saved === 'ru' || saved === 'ky') return saved
  } catch {
    // localStorage недоступен — используем язык по умолчанию
  }
  return 'ru'
}

export const i18n = createI18n({
  legacy: false,
  locale: initialLocale(),
  fallbackLocale: 'ru',
  messages: { ru, ky },
})

export function setLocale(locale: AppLocale) {
  i18n.global.locale.value = locale
  document.documentElement.lang = locale
  try {
    localStorage.setItem(STORAGE_KEY, locale)
  } catch {
    // не критично
  }
}
