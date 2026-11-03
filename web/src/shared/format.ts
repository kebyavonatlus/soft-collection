export type CurrencyCode = 'KGS' | 'USD'

const currencyLabel: Record<CurrencyCode, string> = { KGS: 'сом', USD: '$' }

/** Суммы: пробел между тысячами и два знака после запятой — «12 400,00» (ТЗ, раздел 5). */
export function formatAmount(amount: number): string {
  const fixed = Math.abs(amount).toFixed(2)
  const [intPart = '0', frac = '00'] = fixed.split('.')
  const grouped = intPart.replace(/\B(?=(\d{3})+(?!\d))/g, ' ')
  return `${amount < 0 ? '-' : ''}${grouped},${frac}`
}

export function formatMoney(amount: number, currency: CurrencyCode): string {
  return `${formatAmount(amount)} ${currencyLabel[currency]}`
}

/** Даты — ДД.ММ.ГГГГ (ТЗ, раздел 9). */
export function formatDate(date: Date | string): string {
  const d = typeof date === 'string' ? new Date(date) : date
  const dd = String(d.getDate()).padStart(2, '0')
  const mm = String(d.getMonth() + 1).padStart(2, '0')
  return `${dd}.${mm}.${d.getFullYear()}`
}
