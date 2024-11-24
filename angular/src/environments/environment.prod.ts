import { Environment } from '@abp/ng.core';

const baseUrl = 'https://vccus.net';

const oAuthConfig = {
  issuer: 'https://api.vccus.net/',
  redirectUri: baseUrl,
  clientId: 'VCCUS_CLIENT',
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
      url: 'https://api.vccus.net',
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
