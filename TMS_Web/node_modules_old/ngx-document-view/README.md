# ngx-document-view

Simple document viewer.

## Install

```bash
npm install ngx-document-view
```

## Getting Started

app.module.ts

```typescript
// import module
import { DocumentViewModule } from 'ngx-document-view';

@NgModule({
  declarations: [AppComponent],
  imports: [DocumentViewModule], // import
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {}
```

app.component.html

```html
<iframe style="height: 45vh; width:100%; display: block;"
  ngx-document src="http://unec.edu.az/application/uploads/2014/12/pdf-sample.pdf"
  provider="google"></iframe>
  <!-- provider is Options (google [default] or microsoft)-->
```

### Result

![Imgur](https://i.imgur.com/nntX61Y.gif)
