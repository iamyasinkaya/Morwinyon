# Morwinyon - .NET Platformu için Genişletilmiş NuGet Paketi Uzantısı

**Morwinyon**, geliştiricilere var olan .NET yazılım projelerine ek işlevsellikler kazandırmak için tasarlanmış bir NuGet paketi uzantısıdır. Bu uzantı, genişletilmiş ve özelleştirilmiş özellikler sunarak geliştirme süreçlerini hızlandırır ve yazılım projelerinin kalitesini artırır.

![Durum](https://img.shields.io/badge/durum-aktif-brightgreen)
![Lisans](https://img.shields.io/badge/lisans-MIT-blue)

## Özellikler

- **Exception Handling (İstisna Yönetimi)**: 
  - Kodlarda oluşabilecek istisnai durumları yönetmek için entegre çözümler sunar.
  - İstisnaları yakalamak, günlüğe kaydetmek ve yönetmek için standartlaştırılmış yöntemler sağlar.

- **Versioning (Sürüm Yönetimi)**: 
  - Projenizin farklı sürümlerini kolaylıkla yönetmenizi sağlayan özellikler sunar.
  - API'lerinizi farklı sürümler halinde yönetmenize yardımcı olur.

- **Validation (Doğrulama)**: 
  - Gelen verilerin veya isteklerin doğruluğunu kontrol eder.
  - Kullanıcıdan gelen bilgileri kontrol ederek, yanlış veya hatalı verilerin sisteme girmesini engeller.

- **OpenApi**: 
  - API'nin nasıl kullanılacağını belirlemeye yardımcı olur.
  - Kullanıcıların API'yi daha iyi anlamalarına ve doğru şekilde kullanmalarına yardımcı olur; yani, API'nin kullanım kılavuzu gibi düşünebilirsiniz.

- **Caching (Önbellekleme)**: 
  - Tüm cache yöntemlerini tek bir çatı altında barındırır.
  - Performansı artırmak için sık erişilen verilerin önbelleğe alınmasını sağlar.

## Kurulum

Morwinyon'u yerel ortamınızda kullanmaya başlamak için şu adımları izleyin:

1. Depoyu klonlayın:
    ```bash
    git clone https://github.com/iamyasinkaya/Morwinyon.git
    ```
2. Proje dizinine gidin:
    ```bash
    cd Morwinyon
    ```
3. Gerekli bağımlılıkları yükleyin:
    ```bash
    dotnet restore
    ```

## Kullanım

Morwinyon'un bazı temel özelliklerini kullanmak için örnekler:

### Exception Handling (İstisna Yönetimi)

Global istisna yönetimini uygulamak için:

1. `Startup.cs` dosyanıza istisna yönetimi ara yazılımını ekleyin:
    ```csharp
    app.UseExceptionHandling();
    ```
2. İstisna yanıtlarını özelleştirin:
    ```csharp
    try
    {
        // Kendi kodlarınız
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "Bir hata oluştu");
        throw new CustomException("Özel hata mesajı", ex);
    }
    ```

### Versioning (Sürüm Yönetimi)

API sürüm yönetimini uygulamak için:

1. `Startup.cs` dosyanızda sürüm yönetimini yapılandırın:
    ```csharp
    services.AddApiVersioning(options =>
    {
        options.AssumeDefaultVersionWhenUnspecified = true;
        options.DefaultApiVersion = new ApiVersion(1, 0);
        options.ReportApiVersions = true;
    });
    ```
2. Denetleyicilerinize sürüm yönetimi öznitelikleri ekleyin:
    ```csharp
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class MyController : ControllerBase
    {
        // İşlemler burada
    }
    ```

### Validation (Doğrulama)

Doğrulama uzantı yöntemlerini kullanmak için:

1. Basit veri tiplerini doğrulayın:
    ```csharp
    var isValid = myString.IsValidEmail();
    ```
2. Özel doğrulama kurallarını uygulayın:
    ```csharp
    var validationResult = myObject.ValidateWith(customValidator);
    ```

### OpenApi

API'nizin kullanıcılar tarafından daha iyi anlaşılmasını ve doğru kullanılmasını sağlamak için OpenApi desteğini ekleyin:

1. `Startup.cs` dosyanızda OpenApi'yi etkinleştirin:
    ```csharp
    services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
    });
    ```
2. Uygulamanıza Swagger ara yazılımını ekleyin:
    ```csharp
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });
    ```

### Caching (Önbellekleme)

Önbellekleme özelliğini kullanarak performansınızı artırın:

1. `Startup.cs` dosyanıza önbellekleme hizmetini ekleyin:
    ```csharp
    services.AddMemoryCache();
    ```
2. Hizmetinizde önbellekleme uzantı yöntemini kullanın:
    ```csharp
    var cachedData = await _cache.GetOrCreateAsync("cacheKey", entry =>
    {
        entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5);
        return _yourDataFetchingMethod();
    });
    ```

## Yapılandırma

Morwinyon'u çeşitli ayarlarla özelleştirebilirsiniz:

- **Caching (Önbellekleme)**:
  - `appsettings.json` dosyasında önbellek sürelerini ve stratejilerini ayarlayın.
- **Exception Handling (İstisna Yönetimi)**:
  - Ara yazılım yapılandırması aracılığıyla istisna yönetimi davranışını özelleştirin.
- **Validation (Doğrulama)**:
  - Uygulamanızın ihtiyaçlarına göre doğrulama kurallarını ve stratejilerini yapılandırın.
- **Versioning (Sürüm Yönetimi)**:
  - Sürüm yönetimini öznitelikler ve yapılandırma ayarları aracılığıyla yönetin.

## Katkıda Bulunma

Morwinyon'a katkıda bulunmak isterseniz, lütfen şu adımları izleyin:

1. Depoyu forklayın.
2. Yeni bir özellik veya hata düzeltmesi için bir dal oluşturun (`git checkout -b feature/YourFeatureName`).
3. Değişikliklerinizi commit edin (`git commit -m 'Anlamlı bir commit mesajı ekleyin'`).
4. Dalı push edin (`git push origin feature/YourFeatureName`).
5. `main` dalına bir Pull Request açın.

Kodunuzun kodlama standartlarımıza uygun olduğundan ve uygun testleri içerdiğinden emin olun.

## Lisans

Bu proje MIT Lisansı altında lisanslanmıştır - ayrıntılar için [LICENSE](LICENSE) dosyasına bakın.



## İletişim

Herhangi bir soru, öneri veya geri bildirim için bizimle iletişime geçebilirsiniz:

- **Adı**: Yasin Kaya
- **E-posta**: iamyasinkaya@gmail.com
- **LinkedIn**: [Yasin Kaya](https://www.linkedin.com/in/iamyasinkaya)
