基础插件提供了通用性比较高的前台首页和头部尾部模板.<br/>
如果默认的结构不能满足您的需求, 可以使用[重载模板文件](../core/template_engine)替换所有内容.<br/>

### 前台通用头部

地址: `common.base/header.html`<br/>
结构:
``` html
指定图标 /static/favicon.ico
描画页面关键词(SEO)
描画页面描述(SEO)
描画页面标题
引用css /static/common.base.css/components.css
引用其他要求的css
描画顶部栏 .top-navbar
	描画区域 header_navbar_left
	描画区域 header_navbar_right
描画LOGO栏 .top-logobar
	描画区域 header_logobar
描画菜单栏 .top-menubar
	描画区域 header_menubar
```

### 前台通用尾部

地址: `common.base/footer.html`<br/>
结构:
``` html
描画区域 footer_area_1
描画区域 footer_area_2
描画区域 footer_area_3
引用js /static/common.base.js/components.min.js
引用其他要求的js
```

### 首页

地址: `common.base/index.html`<br/>
结构:
``` html
嵌入 common.base/header.html
描画区域 index_top_area_1
描画区域 index_top_area_2
描画区域 index_top_area_3
描画中间部分 .index-inner-area
	描画区域 index_inner_leftbar
	描画区域 index_inner_middle
	描画区域 index_inner_rightbar
描画区域 index_bottom_area_1
描画区域 index_bottom_area_2
描画区域 index_bottom_area_3
嵌入 common.base/footer.html
```
