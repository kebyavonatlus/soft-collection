<script setup lang="ts">
import { ref, computed } from 'vue'
import { useRouter } from 'vue-router'
import { useI18n } from 'vue-i18n'
import InputText from 'primevue/inputtext'
import Password from 'primevue/password'
import Button from 'primevue/button'
import SelectButton from 'primevue/selectbutton'
import { setLocale, type AppLocale } from '@/i18n'

// Э-01 Вход. Сейчас только вёрстка; проверка на бэкенде — задача S0 «Вход Э-01».
const { t, locale } = useI18n()
const router = useRouter()
const login = ref('')
const password = ref('')

const languageOptions = [
  { label: 'Кыргызча', value: 'ky' },
  { label: 'Русский', value: 'ru' },
]
const currentLocale = computed({
  get: () => locale.value as AppLocale,
  set: (value: AppLocale) => value && setLocale(value),
})

function submit() {
  router.push('/my-cases')
}
</script>

<template>
  <div class="login">
    <section class="promo">
      <div class="brand"><span class="brand-logo">SC</span> Soft Collection</div>
      <div>
        <h1>{{ t('login.tagline') }}</h1>
        <p>{{ t('login.taglineHint') }}</p>
      </div>
      <small>Версия 0.1</small>
    </section>

    <section class="form-side">
      <SelectButton v-model="currentLocale" class="lang" :options="languageOptions" option-label="label" option-value="value" :allow-empty="false" />
      <form class="form" @submit.prevent="submit">
        <h2>{{ t('login.title') }}</h2>
        <label for="login">{{ t('login.login') }}</label>
        <InputText id="login" v-model="login" autocomplete="username" fluid />
        <label for="password">{{ t('login.password') }}</label>
        <Password input-id="password" v-model="password" :feedback="false" toggle-mask fluid />
        <Button type="submit" :label="t('login.submit')" fluid />
        <p class="hint"><i class="pi pi-mobile" /> {{ t('login.smsHint') }}</p>
        <p class="muted">{{ t('login.forgot') }}</p>
      </form>
    </section>
  </div>
</template>

<style scoped>
.login {
  display: grid;
  grid-template-columns: 45% 1fr;
  min-height: 100vh;
}
.promo {
  background: var(--sc-sidebar-bg);
  color: #fff;
  padding: 40px 48px;
  display: flex;
  flex-direction: column;
  justify-content: space-between;
}
.promo h1 {
  font-size: 32px;
  line-height: 1.25;
  margin: 0 0 12px;
}
.promo p,
.promo small {
  color: var(--sc-sidebar-muted);
}
.brand {
  display: flex;
  align-items: center;
  gap: 10px;
  font-weight: 600;
  font-size: 18px;
}
.brand-logo {
  background: #fff;
  color: var(--sc-sidebar-bg);
  border-radius: 8px;
  width: 36px;
  height: 36px;
  display: grid;
  place-items: center;
  font-size: 13px;
  font-weight: 700;
}
.form-side {
  position: relative;
  display: grid;
  place-items: center;
  padding: 24px;
}
.lang {
  position: absolute;
  top: 40px;
  right: 40px;
}
.form {
  width: min(400px, 100%);
  display: flex;
  flex-direction: column;
  gap: 8px;
}
.form h2 {
  font-size: 28px;
  margin: 0 0 16px;
}
.form label {
  font-size: 14px;
  margin-top: 8px;
}
.form :deep(.p-button) {
  margin-top: 16px;
}
.hint {
  background: var(--sc-surface-muted);
  border-radius: 6px;
  padding: 12px 14px;
  font-size: 13px;
  margin-top: 16px;
}
.muted {
  color: var(--sc-text-muted);
  font-size: 13px;
}
@media (max-width: 900px) {
  .login {
    grid-template-columns: 1fr;
  }
  .promo {
    display: none;
  }
}
</style>
