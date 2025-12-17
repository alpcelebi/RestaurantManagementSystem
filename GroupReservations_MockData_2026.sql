-- GroupReservations Tablosu için Mock Veri (20 Adet)
-- Tüm tarihler 2026 yılında

INSERT INTO GroupReservations (ResponsiblePersonName, GroupTitle, ReservationDate, LastProcessDate, Priority, Details, ReservationStatus)
VALUES
('Ahmet Yılmaz', 'Kurumsal Yıl Sonu Yemeği', '2026-12-15 19:00:00', '2026-11-20 10:30:00', 'Yüksek', 'Şirketimizin yıl sonu organizasyonu için 50 kişilik grup rezervasyonu. Özel menü ve müzik talebi var.', 'Pending'),
('Ayşe Demir', 'Düğün Davetiyesi', '2026-06-20 18:00:00', '2026-05-15 14:20:00', 'Yüksek', 'Gelin ve damat için özel düğün yemeği. 80 kişilik kapalı salon rezervasyonu.', 'Approved'),
('Mehmet Kaya', 'Doğum Günü Partisi', '2026-03-10 20:00:00', '2026-02-28 16:45:00', 'Orta', '25. doğum günü kutlaması için 30 kişilik grup. Pasta ve özel dekorasyon isteniyor.', 'Approved'),
('Zeynep Şahin', 'Mezuniyet Yemeği', '2026-07-05 19:30:00', '2026-06-20 11:15:00', 'Orta', 'Üniversite mezuniyet kutlaması. 40 öğrenci ve aileleri için rezervasyon.', 'Pending'),
('Can Öztürk', 'İş Toplantısı Yemeği', '2026-01-25 12:30:00', '2026-01-10 09:00:00', 'Düşük', 'Şirket içi iş toplantısı sonrası öğle yemeği. 15 kişilik masa rezervasyonu.', 'Approved'),
('Elif Arslan', 'Nişan Töreni', '2026-05-14 17:00:00', '2026-04-30 13:30:00', 'Yüksek', 'Nişan töreni için özel salon. 60 kişilik grup, özel menü ve çiçek düzenlemesi.', 'Pending'),
('Burak Çelik', 'Arkadaş Buluşması', '2026-08-22 19:00:00', '2026-08-01 10:20:00', 'Düşük', 'Lise arkadaşları buluşması. 20 kişilik grup rezervasyonu.', 'Approved'),
('Selin Yıldız', 'Bebek Şenliği', '2026-04-18 15:00:00', '2026-04-05 14:00:00', 'Orta', 'Bebek doğum kutlaması. 35 kişilik grup, çocuk dostu ortam isteniyor.', 'Approved'),
('Emre Aydın', 'Futbol Takımı Yemeği', '2026-09-10 20:00:00', '2026-08-25 11:45:00', 'Orta', 'Yerel futbol takımı galibiyet kutlaması. 25 kişilik grup rezervasyonu.', 'Pending'),
('Deniz Kılıç', 'Anma Yemeği', '2026-11-20 18:30:00', '2026-11-05 15:30:00', 'Yüksek', 'Aile anma yemeği. 45 kişilik kapalı salon, özel menü gerekiyor.', 'Approved'),
('Gizem Özkan', 'Konser Sonrası Yemek', '2026-10-12 22:00:00', '2026-09-28 12:00:00', 'Düşük', 'Konser sonrası gece yemeği. 18 kişilik grup, geç saat rezervasyonu.', 'Approved'),
('Onur Tekin', 'Seminer Öğle Yemeği', '2026-02-08 13:00:00', '2026-01-25 10:30:00', 'Düşük', 'Eğitim semineri katılımcıları için öğle yemeği. 22 kişilik grup.', 'Approved'),
('Burcu Yavuz', 'Sünnet Düğünü', '2026-06-28 16:00:00', '2026-06-10 14:15:00', 'Yüksek', 'Sünnet düğünü organizasyonu. 70 kişilik büyük grup, özel dekorasyon ve müzik.', 'Pending'),
('Kerem Doğan', 'İftar Yemeği', '2026-03-25 19:00:00', '2026-03-10 11:00:00', 'Yüksek', 'Ramazan ayı iftar yemeği. 55 kişilik grup, geleneksel menü isteniyor.', 'Approved'),
('Melis Çınar', 'Kız Kına Gecesi', '2026-09-30 19:30:00', '2026-09-15 15:00:00', 'Orta', 'Geleneksel kına gecesi organizasyonu. 38 kişilik kadın grubu.', 'Pending'),
('Tolga Şen', 'İş Ortağı Toplantısı', '2026-01-18 12:00:00', '2026-01-05 09:30:00', 'Düşük', 'İş ortakları ile öğle yemeği toplantısı. 12 kişilik VIP masa.', 'Approved'),
('Pınar Aktaş', 'Yılbaşı Kutlaması', '2026-12-31 20:00:00', '2026-12-15 10:00:00', 'Yüksek', 'Yılbaşı gecesi özel kutlama. 65 kişilik grup, özel menü ve eğlence.', 'Pending'),
('Serkan Yücel', 'Basketbol Takımı Yemeği', '2026-11-05 19:00:00', '2026-10-20 12:30:00', 'Orta', 'Basketbol takımı şampiyonluk kutlaması. 28 kişilik grup rezervasyonu.', 'Approved'),
('Aslı Güven', 'Hediyeleşme Yemeği', '2026-12-10 18:00:00', '2026-11-25 13:45:00', 'Orta', 'Aile içi hediyeleşme geleneği. 32 kişilik aile grubu.', 'Approved'),
('Okan Başar', 'Teknoloji Şirketi Etkinliği', '2026-07-22 19:00:00', '2026-07-05 11:20:00', 'Yüksek', 'Şirket kuruluş yıldönümü kutlaması. 90 kişilik büyük organizasyon, özel etkinlik alanı.', 'Pending');

