// This file can be replaced during build by using the `fileReplacements` array.
// `ng build --prod` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

export const environment = {
  production: false,
  apiUrl: 'http://localhost:5000/api/',
  firebase: {
    apiKey: "AIzaSyAPcB6MkR-vYn_VdZR48G1nae9j231Wm9s",
    authDomain: "englishweb-firebase.firebaseapp.com",
    databaseURL: "https://englishweb-firebase.firebaseio.com",
    projectId: "englishweb-firebase",
    storageBucket: "englishweb-firebase.appspot.com",
    messagingSenderId: "273388055480",
    appId: "1:273388055480:web:d2afcb0205f9220c207d4d",
    measurementId: "G-YZN49S2L47"
  }
};

/*
 * For easier debugging in development mode, you can import the following file
 * to ignore zone related error stack frames such as `zone.run`, `zoneDelegate.invokeTask`.
 *
 * This import should be commented out in production mode because it will have a negative impact
 * on performance if an error is thrown.
 */
// import 'zone.js/dist/zone-error';  // Included with Angular CLI.
