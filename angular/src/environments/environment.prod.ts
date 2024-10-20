import { Environment } from '@abp/ng.core';

const baseUrl = 'https://demo.shop-procard.com';

const oAuthConfig = {
  issuer: 'https://demo.shop-procard.com/',
  redirectUri: baseUrl,
  clientId: 'VCardOnAbp_App',
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
      url: 'https://demo.shop-procard.com',
      rootNamespace: 'VCardOnAbp',
    },
    AbpAccountPublic: {
      url: oAuthConfig.issuer,
      rootNamespace: 'AbpAccountPublic',
    },
  },
  remoteEnv: {
    url: '/getEnvConfig',
    mergeStrategy: 'deepmerge'
  },
  localization: {
    defaultResourceName: "VCardOnAbp",
  },
} as Environment;
