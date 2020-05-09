# cleanPath

[![npm version](https://badge.fury.io/js/cleanpath.svg)](https://badge.fury.io/js/cleanpath)
[![Build Status](https://travis-ci.org/XuPeiYao/cleanPath.svg?branch=master)](https://travis-ci.org/XuPeiYao/cleanPath)
[![Downloads](https://img.shields.io/npm/dm/cleanpath.svg)](https://www.npmjs.com/package/cleanpath)
[![license](https://img.shields.io/github/license/xupeiyao/cleanpath.svg)](https://github.com/XuPeiYao/cleanpath/blob/master/LICENSE)

Clean relative path

## Install

```powershell
npm install cleanpath
```

## Getting Started

```typescript
import { cleanPath } from './cleanPath';

console.assert(cleanPath('C:/A/B/C/D/../123.txt') == 'C:/A/B/C/123.txt');
console.assert(cleanPath('C:/A/B/C/../D/../123.txt') == 'C:/A/B/123.txt');
console.assert(cleanPath('C:/A/B/C/D/./123.txt') == 'C:/A/B/C/D/123.txt');
console.assert(cleanPath('C:/A/B/C/D/../../../../123.txt') == 'C:/123.txt');
console.assert(cleanPath('C:/A/B/C/D//123.txt') == 'C:/A/B/C/D/123.txt');
console.assert(cleanPath('../D/../123.txt') == '../123.txt');
console.assert(cleanPath('../../123.txt') == '../../123.txt');
```
