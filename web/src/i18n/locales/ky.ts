import type ru from './ru'

// Черновой перевод. Перед релизом — вычитка носителем языка (задача в бэклоге).
const ky: typeof ru = {
  app: { name: 'Soft Collection' },
  nav: {
    work: 'Иш',
    analytics: 'Аналитика',
    system: 'Система',
    myCases: 'Менин иштерим',
    promises: 'Убадалар',
    groupCases: 'Топтун иштери',
    approvals: 'Макулдашуулар',
    legal: 'Юридикалык этап',
    dashboard: 'Дашборд',
    reports: 'Отчеттор',
    strategies: 'Стратегиялар',
    integrations: 'Интеграциялар',
    users: 'Колдонуучулар',
    audit: 'Аудит журналы',
  },
  layout: {
    operDay: 'Опер. күн',
    logout: 'Чыгуу',
  },
  login: {
    title: 'Кирүү',
    tagline: 'Мөөнөтү өткөн карыздар менен иштөө — бир терезеде',
    taglineHint: 'Күндүк кезек, карыздардын карточкасы, чалуулар, билдирүүлөр жана төлөө убадалары. Суммалар ар дайым сиздин АБСтен.',
    login: 'Логин',
    password: 'Сырсөз',
    submit: 'Кирүү',
    smsHint: 'Эгер уюмда код менен кирүү күйгүзүлгөн болсо, сырсөздөн кийин телефонуңузга SMS-код келет.',
    forgot: 'Сырсөздү унуттуңузбу же эсеп бөгөттөлдүбү — уюмдун администраторуна кайрылыңыз.',
  },
  placeholder: {
    inProgress: 'Экран иштелип чыгууда',
    plannedIn: 'Пландалган: {milestone}',
  },
  notFound: { title: 'Барак табылган жок' },
}

export default ky
