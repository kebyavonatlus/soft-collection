<script setup lang="ts">
// Э-13 Страница должника по ссылке — этап 4 (ТЗ, раздел 6).
// Отдельная лёгкая сборка без UI-библиотеки: открытие до 3 секунд на мобильном интернете.
// Открывается по личной ссылке /p/{ключ}; данные отдаёт PublicGateway (DMZ).
</script>

<template>
  <main class="page">
    <h1>Soft Collection</h1>
    <p>Страница оплаты появится на этапе 4.</p>
  </main>
</template>

<style scoped>
.page {
  max-width: 480px;
  margin: 0 auto;
  padding: 24px 16px;
  font-family: system-ui, -apple-system, 'Segoe UI', Roboto, sans-serif;
}
</style>
