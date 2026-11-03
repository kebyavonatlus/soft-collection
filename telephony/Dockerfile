# Заготовка образа Asterisk 20 LTS. Конфигурация PJSIP/WebRTC, диалплан, фраза о записи
# и MixMonitor добавляются в спайке этапа 0 и задаче S3 «Asterisk в compose».
FROM debian:bookworm-slim
RUN apt-get update \
 && apt-get install -y --no-install-recommends asterisk \
 && rm -rf /var/lib/apt/lists/*
COPY config/ /etc/asterisk/
CMD ["asterisk", "-f", "-vvv"]
