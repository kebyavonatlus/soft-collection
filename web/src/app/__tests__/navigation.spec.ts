import { describe, expect, it } from 'vitest'
import { mergeAccess, visibleNavigation } from '../navigation'

describe('mergeAccess', () => {
  it('takes the higher level per tab when roles are combined', () => {
    const collector = { myCases: 'full', groupCases: 'none' } as const
    const teamLead = { myCases: 'view', groupCases: 'full', dashboard: 'view' } as const

    expect(mergeAccess(collector, teamLead)).toEqual({
      myCases: 'full',
      groupCases: 'full',
      dashboard: 'view',
    })
  })
})

describe('visibleNavigation', () => {
  it('hides tabs without access and empty sections', () => {
    const nav = visibleNavigation({ myCases: 'full', promises: 'full' })

    expect(nav.map((s) => s.key)).toEqual(['work'])
    expect(nav[0]!.items.map((i) => i.tab)).toEqual(['myCases', 'promises'])
  })
})
