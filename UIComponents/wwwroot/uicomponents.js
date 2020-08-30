/******/ (function(modules) { // webpackBootstrap
/******/ 	// The module cache
/******/ 	var installedModules = {};
/******/
/******/ 	// The require function
/******/ 	function __webpack_require__(moduleId) {
/******/
/******/ 		// Check if module is in cache
/******/ 		if(installedModules[moduleId]) {
/******/ 			return installedModules[moduleId].exports;
/******/ 		}
/******/ 		// Create a new module (and put it into the cache)
/******/ 		var module = installedModules[moduleId] = {
/******/ 			i: moduleId,
/******/ 			l: false,
/******/ 			exports: {}
/******/ 		};
/******/
/******/ 		// Execute the module function
/******/ 		modules[moduleId].call(module.exports, module, module.exports, __webpack_require__);
/******/
/******/ 		// Flag the module as loaded
/******/ 		module.l = true;
/******/
/******/ 		// Return the exports of the module
/******/ 		return module.exports;
/******/ 	}
/******/
/******/
/******/ 	// expose the modules object (__webpack_modules__)
/******/ 	__webpack_require__.m = modules;
/******/
/******/ 	// expose the module cache
/******/ 	__webpack_require__.c = installedModules;
/******/
/******/ 	// define getter function for harmony exports
/******/ 	__webpack_require__.d = function(exports, name, getter) {
/******/ 		if(!__webpack_require__.o(exports, name)) {
/******/ 			Object.defineProperty(exports, name, { enumerable: true, get: getter });
/******/ 		}
/******/ 	};
/******/
/******/ 	// define __esModule on exports
/******/ 	__webpack_require__.r = function(exports) {
/******/ 		if(typeof Symbol !== 'undefined' && Symbol.toStringTag) {
/******/ 			Object.defineProperty(exports, Symbol.toStringTag, { value: 'Module' });
/******/ 		}
/******/ 		Object.defineProperty(exports, '__esModule', { value: true });
/******/ 	};
/******/
/******/ 	// create a fake namespace object
/******/ 	// mode & 1: value is a module id, require it
/******/ 	// mode & 2: merge all properties of value into the ns
/******/ 	// mode & 4: return value when already ns object
/******/ 	// mode & 8|1: behave like require
/******/ 	__webpack_require__.t = function(value, mode) {
/******/ 		if(mode & 1) value = __webpack_require__(value);
/******/ 		if(mode & 8) return value;
/******/ 		if((mode & 4) && typeof value === 'object' && value && value.__esModule) return value;
/******/ 		var ns = Object.create(null);
/******/ 		__webpack_require__.r(ns);
/******/ 		Object.defineProperty(ns, 'default', { enumerable: true, value: value });
/******/ 		if(mode & 2 && typeof value != 'string') for(var key in value) __webpack_require__.d(ns, key, function(key) { return value[key]; }.bind(null, key));
/******/ 		return ns;
/******/ 	};
/******/
/******/ 	// getDefaultExport function for compatibility with non-harmony modules
/******/ 	__webpack_require__.n = function(module) {
/******/ 		var getter = module && module.__esModule ?
/******/ 			function getDefault() { return module['default']; } :
/******/ 			function getModuleExports() { return module; };
/******/ 		__webpack_require__.d(getter, 'a', getter);
/******/ 		return getter;
/******/ 	};
/******/
/******/ 	// Object.prototype.hasOwnProperty.call
/******/ 	__webpack_require__.o = function(object, property) { return Object.prototype.hasOwnProperty.call(object, property); };
/******/
/******/ 	// __webpack_public_path__
/******/ 	__webpack_require__.p = "";
/******/
/******/
/******/ 	// Load entry module and return exports
/******/ 	return __webpack_require__(__webpack_require__.s = "./src/scripts/index.ts");
/******/ })
/************************************************************************/
/******/ ({

/***/ "./src/scripts/dateTimeHelpers.ts":
/*!****************************************!*\
  !*** ./src/scripts/dateTimeHelpers.ts ***!
  \****************************************/
/*! exports provided: DateTimeHelper */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
eval("__webpack_require__.r(__webpack_exports__);\n/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, \"DateTimeHelper\", function() { return DateTimeHelper; });\nclass DateTimeHelper {\r\n    getTimeZoneOffset() {\r\n        return new Date().getTimezoneOffset();\r\n    }\r\n}\r\n//# sourceURL=[module]\n//# sourceMappingURL=data:application/json;charset=utf-8;base64,eyJ2ZXJzaW9uIjozLCJmaWxlIjoiLi9zcmMvc2NyaXB0cy9kYXRlVGltZUhlbHBlcnMudHMuanMiLCJzb3VyY2VzIjpbIndlYnBhY2s6Ly8vLi9zcmMvc2NyaXB0cy9kYXRlVGltZUhlbHBlcnMudHM/OTI3YSJdLCJzb3VyY2VzQ29udGVudCI6WyJleHBvcnQgY2xhc3MgRGF0ZVRpbWVIZWxwZXIge1xyXG4gICAgZ2V0VGltZVpvbmVPZmZzZXQoKTogbnVtYmVyIHtcclxuICAgICAgICByZXR1cm4gbmV3IERhdGUoKS5nZXRUaW1lem9uZU9mZnNldCgpO1xyXG4gICAgfVxyXG59Il0sIm1hcHBpbmdzIjoiQUFBQTtBQUFBO0FBQUE7QUFDQTtBQUNBO0FBQ0E7QUFDQTsiLCJzb3VyY2VSb290IjoiIn0=\n//# sourceURL=webpack-internal:///./src/scripts/dateTimeHelpers.ts\n");

/***/ }),

/***/ "./src/scripts/domHelpers.ts":
/*!***********************************!*\
  !*** ./src/scripts/domHelpers.ts ***!
  \***********************************/
/*! exports provided: DOMHelpers */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
eval("__webpack_require__.r(__webpack_exports__);\n/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, \"DOMHelpers\", function() { return DOMHelpers; });\nclass DOMHelpers {\r\n    scrollIntoView(cssSelector) {\r\n        const element = document.querySelector(cssSelector);\r\n        if (element == null) {\r\n            throw new Error('Element not found');\r\n        }\r\n        element.scrollIntoView({ behavior: 'smooth' });\r\n    }\r\n    getBoundingClientRect(element) {\r\n        return element.getBoundingClientRect();\r\n    }\r\n}\r\n;\r\n//# sourceURL=[module]\n//# sourceMappingURL=data:application/json;charset=utf-8;base64,eyJ2ZXJzaW9uIjozLCJmaWxlIjoiLi9zcmMvc2NyaXB0cy9kb21IZWxwZXJzLnRzLmpzIiwic291cmNlcyI6WyJ3ZWJwYWNrOi8vLy4vc3JjL3NjcmlwdHMvZG9tSGVscGVycy50cz8yZjVkIl0sInNvdXJjZXNDb250ZW50IjpbImV4cG9ydCBjbGFzcyBET01IZWxwZXJzIHtcclxuICAgIC8qKlxyXG4gICAgICogU2Nyb2xsIHRvIGVsZW1lbnRcclxuICAgICAqIEBwYXJhbSBjc3NTZWxlY3RvclxyXG4gICAgICovXHJcbiAgICBzY3JvbGxJbnRvVmlldyhjc3NTZWxlY3Rvcjogc3RyaW5nKTogdm9pZCB7XHJcbiAgICAgICAgY29uc3QgZWxlbWVudCA9IGRvY3VtZW50LnF1ZXJ5U2VsZWN0b3IoY3NzU2VsZWN0b3IpO1xyXG4gICAgICAgIGlmIChlbGVtZW50ID09IG51bGwpIHtcclxuICAgICAgICAgICAgdGhyb3cgbmV3IEVycm9yKCdFbGVtZW50IG5vdCBmb3VuZCcpO1xyXG4gICAgICAgIH1cclxuICAgICAgICBlbGVtZW50LnNjcm9sbEludG9WaWV3KHsgYmVoYXZpb3I6ICdzbW9vdGgnIH0pO1xyXG4gICAgfVxyXG5cclxuICAgIGdldEJvdW5kaW5nQ2xpZW50UmVjdChlbGVtZW50OiBIVE1MRWxlbWVudCk6IERPTVJlY3Qge1xyXG4gICAgICAgIHJldHVybiBlbGVtZW50LmdldEJvdW5kaW5nQ2xpZW50UmVjdCgpO1xyXG4gICAgfVxyXG59O1xyXG5cclxuXHJcbiJdLCJtYXBwaW5ncyI6IkFBQUE7QUFBQTtBQUFBO0FBS0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFFQTtBQUNBO0FBQ0E7QUFDQTtBQUFBOyIsInNvdXJjZVJvb3QiOiIifQ==\n//# sourceURL=webpack-internal:///./src/scripts/domHelpers.ts\n");

/***/ }),

/***/ "./src/scripts/index.ts":
/*!******************************!*\
  !*** ./src/scripts/index.ts ***!
  \******************************/
/*! no exports provided */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
eval("__webpack_require__.r(__webpack_exports__);\n/* harmony import */ var _dateTimeHelpers__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ./dateTimeHelpers */ \"./src/scripts/dateTimeHelpers.ts\");\n/* harmony import */ var _domHelpers__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ./domHelpers */ \"./src/scripts/domHelpers.ts\");\n\r\n\r\nwindow.UIComponents = {\r\n    DOM: new _domHelpers__WEBPACK_IMPORTED_MODULE_1__[\"DOMHelpers\"](),\r\n    Date: new _dateTimeHelpers__WEBPACK_IMPORTED_MODULE_0__[\"DateTimeHelper\"](),\r\n};\r\n//# sourceURL=[module]\n//# sourceMappingURL=data:application/json;charset=utf-8;base64,eyJ2ZXJzaW9uIjozLCJmaWxlIjoiLi9zcmMvc2NyaXB0cy9pbmRleC50cy5qcyIsInNvdXJjZXMiOlsid2VicGFjazovLy8uL3NyYy9zY3JpcHRzL2luZGV4LnRzP2U4NDgiXSwic291cmNlc0NvbnRlbnQiOlsiaW1wb3J0IHsgRGF0ZVRpbWVIZWxwZXIgfSBmcm9tIFwiLi9kYXRlVGltZUhlbHBlcnNcIjtcclxuaW1wb3J0IHsgRE9NSGVscGVycyB9IGZyb20gJy4vZG9tSGVscGVycyc7XHJcbi8vIEB0cy1pZ25vcmVcclxud2luZG93LlVJQ29tcG9uZW50cyA9IHtcclxuICAgIERPTTogbmV3IERPTUhlbHBlcnMoKSxcclxuICAgIERhdGU6IG5ldyBEYXRlVGltZUhlbHBlcigpLFxyXG59Il0sIm1hcHBpbmdzIjoiQUFBQTtBQUFBO0FBQUE7QUFBQTtBQUNBO0FBRUE7QUFDQTtBQUNBO0FBQ0E7Iiwic291cmNlUm9vdCI6IiJ9\n//# sourceURL=webpack-internal:///./src/scripts/index.ts\n");

/***/ })

/******/ });