// The file contents for the current environment will overwrite these during build.
// The build system defaults to the dev environment which uses `environment.ts`, but if you do
// `ng build --env=prod` then `environment.prod.ts` will be used instead.
// The list of which env maps to which file can be found in `.angular-cli.json`.

export const environment = {
  production: false,
  apiUrl : 'https://localhost:44348/',
  openIdConnectSettings : {
    authority : 'https://localhost:44352/',
    client_id : 'phsom-web-client',
    redirect_uri : 'https://localhost:4201/signin-oidc',
    scope : 'openid profile roles',
    response_type : 'id_token',
    post_logout_redirect_uri : 'https://localhost:4201'
  }
};
