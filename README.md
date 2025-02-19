Online Quiz System API

📌 Proje Açıklaması

Online Quiz System API, çevrimiçi bir quiz (sınav) sistemi için geliştirilmiş bir backend API’sidir. Kullanıcıların quiz oluşturmasına, quizlere katılmasına ve sonuçları görmesine olanak tanır. Session yönetimi için Redis kullanılmıştır ve oturum verileri belirli bir süre sonra MySQL veritabanına aktarılır.

🚀 Kullanılan Teknolojiler

Bu projede aşağıdaki teknolojiler ve kütüphaneler kullanılmıştır:

Backend: .NET (ASP.NET Core)

Dil: C#

Veritabanı: MySQL (Pomelo MySQL)

Cache: Redis (StackExchange.Redis)

ORM: Entity Framework Core

Paketler:

AutoMapper

ASP.NET MVC

Entity Framework Core, Entity Design, Entity Tools

StackExchange.Redis (Redis için)

Pomelo MySQL

⚡ Kurulum

Projeyi çalıştırmak için aşağıdaki adımları takip edebilirsin:

Gereksinimleri yükle:

.NET SDK (uygun sürüm)

MySQL Server

Redis Server

Bağımlılıkları yükle:

dotnet restore

Veritabanını hazırla:

MySQL’de bir veritabanı oluştur.

appsettings.json dosyasında MySQL bağlantı bilgilerini güncelle.

Projeyi çalıştır:

dotnet run

🎯 Kullanım

Quiz oluşturabilir, silebilir ve güncelleyebilirsin.

Kullanıcılar quizlere katılabilir ve sonuçları alabilir.

Session verileri Redis’e kaydedilir ve süre sonunda MySQL’e aktarılır.

🔥 Katkıda Bulunma

Bu proje kişisel bir proje olarak geliştirilmiştir, ancak kodu isteyen alıp kullanabilir.

📜 Lisans

Bu proje herhangi bir lisans içermemektedir.
