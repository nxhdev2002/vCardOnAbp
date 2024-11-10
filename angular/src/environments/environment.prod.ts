import { Environment } from '@abp/ng.core';

const baseUrl = 'https://fe.hoangnx.info';

const oAuthConfig = {
  issuer: 'https://vcard.hoangnx.info/',
  redirectUri: baseUrl,
  clientId: 'VCard_Angular',
  responseType: 'code',
  scope: 'offline_access VCardOnAbp',
  requireHttps: true,
};

export const environment = {
  production: true,
  application: {
    baseUrl,
    name: 'VCardOnAbp',
  },
  oAuthConfig,
  apis: {
    default: {
      url: 'https://vcard.hoangnx.info',
      rootNamespace: 'VCardOnAbp',
    },
    AbpAccountPublic: {
      url: oAuthConfig.issuer,
      rootNamespace: 'AbpAccountPublic',
    },
  },

  localization: {
    defaultResourceName: "VCardOnAbp",
  },
} as Environment;
