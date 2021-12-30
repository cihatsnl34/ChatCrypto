class dilKontrol:
    def __init__(self,text):
        self.text=text
    def CumleSayisi(text):
       satirlar=text.split(".")
       b=0
       for i in satirlar:
           b=b+1
       return b-1
    def Kelimesayisi(text):
        kelimeler = text.split(" ")
        b=0
        for i in kelimeler:
            b=b+1
        return b
    def Sesliharfbul(text):
        sesli_harf=["a","e","i","ı","o","ü","u","ö"]
        b=0
        for i in text.lower():
            for s in sesli_harf:
                if i == s:
                    b+=1
        return b
    def Unluuyumu(text):
        kalin_unluler = list("aıou") 
        ince_unluler = list("eiöü")
        kelimeArr=text.split(" ")
        b=0
        for kelime in kelimeArr:
            if (sum(kelime.count(kalin) for kalin in kalin_unluler)) != 0 or (sum(kelime.count(ince) for ince in ince_unluler)) != 0:  
               b+=1
        return b
try:
    paragraf=input("Cümle Giriniz: ").lower()
    cumle=dilKontrol.CumleSayisi(paragraf)
    kelime=dilKontrol.Kelimesayisi(paragraf)
    SesliHarf=dilKontrol.Sesliharfbul(paragraf)
    Unluuyumu=dilKontrol.Unluuyumu(paragraf)
    print("Cümle Sayısı: "+ str(cumle))
    print("Kelime Sayı: "+str(kelime))
    print("Sesli Harf Sayı: "+str(SesliHarf))
    print("Ünlü Uyumumuna Uyan Kelime Sayısı: "+str(Unluuyumu))
except RuntimeError:
    print("Program çalışırken beklenmedik bir hata ile karşılaşıldı")
                    


