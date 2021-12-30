import hashlib
import sys
from cryptography.fernet import Fernet

class sifrelemeYontemleri:
    def __init__(self,txt):
        self.txt=txt
    def Sifrelesha256(txt):
        result=hashlib.sha256( txt.encode())
        return result.hexdigest()
    def Sifrelesha224(txt):
        result=hashlib.sha224( txt.encode())
        return result.hexdigest()
    def Sifrelesha384(txt):
        result=hashlib.sha384( txt.encode())
        return result.hexdigest()
    def Sifrelesha512(txt):
        result=hashlib.sha512( txt.encode())
        return result.hexdigest()          


class sifrelemeYontemleri2:
    def Symmetric_encryption(txt,f):
        key=Fernet(f)
        token=key.encrypt(txt.encode())
        return token
    def Symmetric_decryption(txt, f):
        token = Fernet(f)
        yazi = token.decrypt(txt.encode())
        return yazi.decode()
    def generate_Key():
        f=Fernet.generate_key()
        return f

class switchs: 
    def switch_demo2(islem, fun, text):
        if fun == '0':
            return switchs.switch_demo(fun,text)
        elif fun == '1':
            return switchs.switch_demo1(fun,text)
        else:
            return

    def switch_demo(fun, text):
        switcher = {
            '0': str(sifrelemeYontemleri.Sifrelesha224(text)), #sha224
            '1': str(sifrelemeYontemleri.Sifrelesha256(text)), #sha256
            '2': str(sifrelemeYontemleri.Sifrelesha384(text)), #sha384
            '3': str(sifrelemeYontemleri.Sifrelesha512(text)) #sha512
        }
        return switcher.get(fun, "none")

    def switch_demo1(fun, text):
        if fun == '0':
            new_text = text.split("'")
            return str(sifrelemeYontemleri2.Symmetric_decryption(new_text[1],"qXprqdb_4ItWUl5mUjvbC9AzBe7p0hE_JZE_ZeGrdfQ="))
        elif fun == '1':
            return str(sifrelemeYontemleri2.Symmetric_encryption(text,"qXprqdb_4ItWUl5mUjvbC9AzBe7p0hE_JZE_ZeGrdfQ="))
        else: 
            return str(sifrelemeYontemleri2.generate_Key()) #generate key






def main(argv):
    new_text = switchs.switch_demo2(argv[0],argv[1],argv[2])
    print(new_text)
    sys.stdout.flush()

    

if __name__ == "__main__":
   main(sys.argv[1:])