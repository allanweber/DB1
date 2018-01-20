import { AppConfig } from './app.config';

export class AppEnvironment {
  private baseApi: string;

  constructor(private appConfig: AppConfig) {
    this.baseApi = this.appConfig.getConfig('appSettings').gitApiUrl;
  }

  public userApi = {
    getAll: (): string => `${this.baseApi}/api/v1/User`,
    getUser: (userName: string): string =>
      `${this.baseApi}/api/v1/User/${userName}`
  };

  public repoApi = {
    getByUser: (userName: string): string =>
      `${this.baseApi}/api/v1/Repository/${userName}`
  };
}

export function AppEnvironmentFactory(appConfig: AppConfig) {
  return new AppEnvironment(appConfig);
}
