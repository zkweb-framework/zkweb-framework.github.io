验证码插件包括了验证码的显示和验证功能.<br/>
默认会显示4位英文+数字验证码（除去难以辨认的I和O等）.<br/>

### 表单中的验证码

添加以下内容到表单即可, 提交时会自动验证.<br/>
注意不添加[Required]也会进行验证.<br/>
```csharp
[CaptchaField("Captcha", "Example.Captcha")]
public string Captcha { get; set; }
```

### 验证码管理器

需要手动管理验证码时可以使用`CaptchaManager`.

- `CaptchaManager.Generate` (生成验证码图片)
- `CaptchaManager.GetWithoutRemove` (获取当前验证码, 但不进行删除)
- `CaptchaManager.Check` (判断验证码是否正确, 并删除当前验证码)
- `CaptchaManager.GetAudioStream` (获取当前验证码的语音)

### 验证码语音

生成验证码语音需要进程池属于有权限的本地用户（IIS用户没有这个权限）.<br/>
如果需要关闭语音支持请在网站配置的`Extra`节下添加`"Common.Captcha.SupportCaptchaAudio": false`.<br/>
