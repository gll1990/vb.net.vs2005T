''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
' Copyright (C) 2014, SafeNet, Inc. All rights reserved.
'
'
'
' 
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Public Class VendorCode

    'The Base64 encoded HASP vendor code for demo keys.
    Private Const vendorCodeString As String = _
                         "upuWuwbFaz7olOs47G8S6DSqfla04At8AOqpwnxrFWQRYO54kskpy9M27k8oSWE3on3r3Xn+1zmLegzF" +
                         "TUOS31ekaRVWmGgGrLPMAbKLHP+fa2PUWmrywxuG/BCe5otS7O+Sp6aRA9lPZQtWIsG2DPokXCQvPxUr" +
                         "tK2QzQj+DR80NkgzJV3f63VzzD/Uw+FBBhDNSKOj5fQgTpxlMNKXX6G8h+EF4mMXWaU4ur9f8mEK1oKG" +
                          "scDQdNxOY0G4FxHOwrzOw46824S+EdhNNmWqaY96jG0JWong/Ftmp2VBkgT4caxa21qTidN6wIfBXE9M" +
                          "qDV6Ka71k61O9u5ejwOXFXJK/QztbVTHbCOyz6JzlLziagByhEnSpyKhH+FfTFaQxrUbZccTAqAPS4C0" +
                           "6BtQOsft0TuhDPvQZ8upQioYOvhavOUT0fR1wm3DCdMqydf5S2RERHIM1zWX0oXfM6mjQR0MKdmy4jYv" +
                         "GBCxDj7M0+ajAUkqQebl6M0WXgakKt9G4+1vqxGl8ifsoVQs8Yjsm11kJzSkQdg1MTaWvTvrPq5rxlIF" +
                         "E2z6OCUclU3Y7MURQ/OXJmWWZWKHsyx13EdrGyU5zrYoGENiWa1G5UzArU/ZTLlWug2lVNIv4CPbukiD" +
                         "6Cq1V7HDslPP23SlxiLPuAmKLzo7e0ddOJHn4IswNCZbbAOCe919QmOb3yIb7va5yxDfW6k/6Nwa3odV" +
                         "53I/1inoNGHRCW1SHWXfLvGgS3mSx2uIUZz7/luRwlRgJFJCI16vlOUkzutbVAgUO3r5UtVOMG0dHV1m" +
                         "5dBaLn1uMn6Pr+o365rO2AMZ7Zz8ETZmLno6a6XNLMNQfMdy/BI1Cy4kTRqk0bK3c3tIs0cML7UXeQ8H" +
                         "XBOgPicHHx7jlGB/7cjJ8+TWeTjNcpozzBh9kW3NgMhGWoWUljEMPgp35KnbSUh918Vj84J7fzZaAevj" +
                         "33SuywwRkkTgzizatvp0WA=="

    'Returns the vendor code as a String.
    Public Shared ReadOnly Property Code() As String
        Get
            Return vendorCodeString
        End Get
    End Property
End Class

