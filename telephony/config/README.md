# Конфигурация Asterisk

Сюда кладутся `pjsip.conf`, `extensions.conf`, `ari.conf`, `http.conf`.
Схема: браузер (JsSIP, WSS) → Asterisk (PJSIP + WebRTC) → SIP-транк оператора.
Бэкенд управляет вызовами через ARI: проверка contact-guard → вызов → фраза о записи
на языке клиента → MixMonitor → запись привязывается к касанию.
