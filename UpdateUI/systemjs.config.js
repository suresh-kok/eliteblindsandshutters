/**
 * System configuration for Angular 2 samples
 * Adjust as necessary for your application needs.
 */
(function(global) {
  // map tells the System loader where to look for things
  var map = {
    'app':                        'app', // 'dist',
    '@angular':                   'node_modules/@angular',
    '@angular/common/http':       'node_modules/@angular/common/bundles',
    'angular2-in-memory-web-api': 'node_modules/angular2-in-memory-web-api',
    'rxjs':                       'node_modules/rxjs',
    'tslib':                      'node_modules/tslib', 
    'angular4-oauth-login':       'node_modules/angular4-oauth-login/src',
    // 'angular4-social-login':       'node_modules/angular4-social-login',
    'plugin-babel': 'path/to/systemjs-plugin-babel/plugin-babel.js',
    'systemjs-babel-build': 'path/to/systemjs-plugin-babel/systemjs-babel-browser.js',
    'angular2-google-login': 'node_modules/angular2-google-login',
  };
  // packages tells the System loader how to load when no filename and/or no extension
  var packages = {
    'app':                        { main: 'main.js',  defaultExtension: 'js' },
    'rxjs':                       { defaultExtension: 'js' },
    'angular2-in-memory-web-api': { main: 'index.js', defaultExtension: 'js' },
    '@angular/common/http':       { main: 'common-http.umd.js',  defaultExtension: 'js' },
    'tslib':                      { main: 'tslib.js',  defaultExtension: 'js' },
    'angular4-oauth-login':       { main:'index.js',defaultExtension:'js'},
    // 'angular4-social-login':       { main:'index.js',defaultExtension:'js'},
    // 'angular2-google-login':      {main:'index.js',defaultExtension:'js'},
  };
  var ngPackageNames = [
    'common',
    'compiler',
    'core',
    'forms',
    'http',
    'platform-browser',
    'platform-browser-dynamic',
    'router',
    'router-deprecated',
    'upgrade',
  ];
  
  // Individual files (~300 requests):
  function packIndex(pkgName) {
    packages['@angular/'+pkgName] = { main: 'index.js', defaultExtension: 'js' };
  }
  // Bundled (~40 requests):
  function packUmd(pkgName) {
    packages['@angular/'+pkgName] = { main: 'bundles/' + pkgName + '.umd.js', defaultExtension: 'js' };
  }
  // Most environments should use UMD; some (Karma) need the individual index files
  var setPackageConfig = System.packageWithIndex ? packIndex : packUmd;
  // Add package entries for angular packages
  ngPackageNames.forEach(setPackageConfig);
  var config = {
    map: map,
    packages: packages
  };
  System.config(config);
})(this);