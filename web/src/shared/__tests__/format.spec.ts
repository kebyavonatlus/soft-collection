import { describe, expect, it } from 'vitest'
import { formatAmount, formatDate, formatMoney } from '../format'

describe('formatAmount', () => {
  it.each([
    [12400, '12 400,00'],
    [3100.5, '3 100,50'],
    [0, '0,00'],
    [1234567.89, '1 234 567,89'],
    [-500, '-500,00'],
  ])('%d → %s', (amount, expected) => {
    expect(formatAmount(amount)).toBe(expected)
  })

  it('adds currency label', () => {
    expect(formatMoney(46200, 'KGS')).toBe('46 200,00 сом')
  })
})

describe('formatDate', () => {
  it('formats as DD.MM.YYYY', () => {
    expect(formatDate(new Date(2026, 9, 27))).toBe('27.10.2026')
  })
})
