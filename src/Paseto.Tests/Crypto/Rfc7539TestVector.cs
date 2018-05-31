﻿namespace Paseto.Tests.Crypto
{
    using Cryptography;

    public class Rfc7539TestVector
    {
        public byte[] Key { get; private set; }
        public byte[] PlainText { get; private set; }
        public byte[] Nonce { get; private set; }
        public byte[] CipherText { get; private set; }
        public int InitialCounter { get; private set; }
        public byte[] Aad { get; private set; }
        public byte[] Tag { get; private set; }

        public Rfc7539TestVector(string key, string plaintext, string nonce, string ciphertext, int initialCounter)
        {
            Key = CryptoBytes.FromHexString(key);
            PlainText = CryptoBytes.FromHexString(plaintext);
            Nonce = CryptoBytes.FromHexString(nonce);
            CipherText = CryptoBytes.FromHexString(ciphertext);
            InitialCounter = initialCounter;
        }

        public Rfc7539TestVector(string plaintext, string aad, string key, string nonce, string ciphertext, string tag)
        {
            PlainText = CryptoBytes.FromHexString(plaintext);
            Aad = CryptoBytes.FromHexString(aad);
            Key = CryptoBytes.FromHexString(key);
            Nonce = CryptoBytes.FromHexString(nonce);
            CipherText = CryptoBytes.FromHexString(ciphertext);
            Tag = CryptoBytes.FromHexString(tag);
        }

        public static Rfc7539TestVector[] Rfc7539TestVectors =
        {
            // Tests against the test vectors in Section 2.3.2 of RFC 7539.
            // https://tools.ietf.org/html/rfc7539#section-2.3.2
            new Rfc7539TestVector(
                "000102030405060708090a0b0c0d0e0f101112131415161718191a1b1c1d1e1f",
                "4c616469657320616e642047656e746c656d656e206f662074686520636c617373206f66202739393a20496620"
                    + "4920636f756c64206f6666657220796f75206f6e6c79206f6e652074697020666f722074686520667574"
                    + "7572652c2073756e73637265656e20776f756c642062652069742e",
                "000000000000004a00000000",
                "6e2e359a2568f98041ba0728dd0d6981e97e7aec1d4360c20a27afccfd9fae0bf91b65c5524733ab8f593dabcd62b3571639d624e65152ab8f530c359f0861d807ca0dbf500d6a6156a38e088a22b65e52bc514d16ccf806818ce91ab77937365af90bbf74a35be6b40b8eedf2785e42874d",
                1),
            new Rfc7539TestVector(
                "0000000000000000000000000000000000000000000000000000000000000000",
                "000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000"
                    + "00000000000000000000000000000000000000",
                "000000000000000000000000",
                "76b8e0ada0f13d90405d6ae55386bd28bdd219b8a08ded1aa836efcc8b770dc7da41597c5157488d7724e03fb8d84a376a43b8f41518a11cc387b669b2ee6586",
                0),
            new Rfc7539TestVector(
                "0000000000000000000000000000000000000000000000000000000000000001",
                "416e79207375626d697373696f6e20746f20746865204945544620696e74656e6465642062792074686520436f"
                    + "6e7472696275746f7220666f72207075626c69636174696f6e20617320616c6c206f722070617274206f"
                    + "6620616e204945544620496e7465726e65742d4472616674206f722052464320616e6420616e79207374"
                    + "6174656d656e74206d6164652077697468696e2074686520636f6e74657874206f6620616e2049455446"
                    + "20616374697669747920697320636f6e7369646572656420616e20224945544620436f6e747269627574"
                    + "696f6e222e20537563682073746174656d656e747320696e636c756465206f72616c2073746174656d65"
                    + "6e747320696e20494554462073657373696f6e732c2061732077656c6c206173207772697474656e2061"
                    + "6e6420656c656374726f6e696320636f6d6d756e69636174696f6e73206d61646520617420616e792074"
                    + "696d65206f7220706c6163652c207768696368206172652061646472657373656420746f",
                "000000000000000000000002",
                "a3fbf07df3fa2fde4f376ca23e82737041605d9f4f4f57bd8cff2c1d4b7955ec2a97948bd3722915c8f3d337f7d370050e9e96d647b7c39f56e031ca5eb6250d4042e02785ececfa4b4bb5e8ead0440e20b6e8db09d881a7c6132f420e52795042bdfa7773d8a9051447b3291ce1411c680465552aa6c405b7764d5e87bea85ad00f8449ed8f72d0d662ab052691ca66424bc86d2df80ea41f43abf937d3259dc4b2d0dfb48a6c9139ddd7f76966e928e635553ba76c5c879d7b35d49eb2e62b0871cdac638939e25e8a1e0ef9d5280fa8ca328b351c3c765989cbcf3daa8b6ccc3aaf9f3979c92b3720fc88dc95ed84a1be059c6499b9fda236e7e818b04b0bc39c1e876b193bfe5569753f88128cc08aaa9b63d1a16f80ef2554d7189c411f5869ca52c5b83fa36ff216b9c1d30062bebcfd2dc5bce0911934fda79a86f6e698ced759c3ff9b6477338f3da4f9cd8514ea9982ccafb341b2384dd902f3d1ab7ac61dd29c6f21ba5b862f3730e37cfdc4fd806c22f221",
                1),
            new Rfc7539TestVector(
                "1c9240a5eb55d38af333888604f6b5f0473917c1402b80099dca5cbc207075c0",
                "2754776173206272696c6c69672c20616e642074686520736c6974687920746f7665730a446964206779726520616e642067696d626c6520696e2074686520776162653a0a416c6c206d696d737920776572652074686520626f726f676f7665732c0a416e6420746865206d6f6d65207261746873206f757467726162652e",
                "000000000000000000000002",
                "62e6347f95ed87a45ffae7426f27a1df5fb69110044c0d73118effa95b01e5cf166d3df2d721caf9b21e5fb14c616871fd84c54f9d65b283196c7fe4f60553ebf39c6402c42234e32a356b3e764312a61a5532055716ead6962568f87d3f3f7704c6a8d1bcd1bf4d50d6154b6da731b187b58dfd728afa36757a797ac188d1",
                42),
            // Tests against the test vectors in Section 2.6.2 of RFC 7539.
            // https://tools.ietf.org/html/rfc7539#section-2.6.2
            new Rfc7539TestVector(
                "808182838485868788898a8b8c8d8e8f909192939495969798999a9b9c9d9e9f",
                "0000000000000000000000000000000000000000000000000000000000000000",
                "000000000001020304050607",
                "8ad5a08b905f81cc815040274ab29471a833b637e3fd0da508dbb8e2fdd1a646",
                0),
            new Rfc7539TestVector(
                "0000000000000000000000000000000000000000000000000000000000000000",
                "0000000000000000000000000000000000000000000000000000000000000000",
                "000000000000000000000000",
                "76b8e0ada0f13d90405d6ae55386bd28bdd219b8a08ded1aa836efcc8b770dc7",
                0),
            new Rfc7539TestVector(
                "0000000000000000000000000000000000000000000000000000000000000001",
                "0000000000000000000000000000000000000000000000000000000000000000",
                "000000000000000000000002",
                "ecfa254f845f647473d3cb140da9e87606cb33066c447b87bc2666dde3fbb739",
                0),
            new Rfc7539TestVector(
                "1c9240a5eb55d38af333888604f6b5f0473917c1402b80099dca5cbc207075c0",
                "0000000000000000000000000000000000000000000000000000000000000000",
                "000000000000000000000002",
                "965e3bc6f9ec7ed9560808f4d229f94b137ff275ca9b3fcbdd59deaad23310ae",
                0),
        };

        public static Rfc7539TestVector[] Rfc7539AeadTestVectors =
        {
            // Section 2.8.2
            // Example and Test Vector for AEAD_CHACHA20_POLY1305
            // https://tools.ietf.org/html/rfc7539#section-2.8.2
            new Rfc7539TestVector(
                "4c616469657320616e642047656e746c656d656e206f662074686520636c617373206f66202739393a204966204920636f756c64206f6666657220796f75206f6e6c79206f6e652074697020666f7220746865206675747572652c2073756e73637265656e20776f756c642062652069742e",
                "50515253c0c1c2c3c4c5c6c7",
                "808182838485868788898a8b8c8d8e8f909192939495969798999a9b9c9d9e9f",
                "07000000" + "4041424344454647",
                "d31a8d34648e60db7b86afbc53ef7ec2a4aded51296e08fea9e2b5a736ee62d63dbea45e8ca9671282fafb69da92728b1a71de0a9e060b2905d6a5b67ecd3b3692ddbd7f2d778b8c9803aee328091b58fab324e4fad675945585808b4831d7bc3ff4def08e4b7a9de576d26586cec64b6116",
                "1ae10b594f09e26a7e902ecbd0600691"),
            // Appendix A.5
            new Rfc7539TestVector(
                "496e7465726e65742d4472616674732061726520647261667420646f63756d656e74732076616c696420666f722061206d6178696d756d206f6620736978206d6f6e74687320616e64206d617920626520757064617465642c207265706c616365642c206f72206f62736f6c65746564206279206f7468657220646f63756d656e747320617420616e792074696d652e20497420697320696e617070726f70726961746520746f2075736520496e7465726e65742d447261667473206173207265666572656e6365206d6174657269616c206f7220746f2063697465207468656d206f74686572207468616e206173202fe2809c776f726b20696e2070726f67726573732e2fe2809d",
                "f33388860000000000004e91",
                "1c9240a5eb55d38af333888604f6b5f0473917c1402b80099dca5cbc207075c0",
                "000000000102030405060708",
                "64a0861575861af460f062c79be643bd5e805cfd345cf389f108670ac76c8cb24c6cfc18755d43eea09ee94e382d26b0bdb7b73c321b0100d4f03b7f355894cf332f830e710b97ce98c8a84abd0b948114ad176e008d33bd60f982b1ff37c8559797a06ef4f0ef61c186324e2b3506383606907b6a7c02b0f9f6157b53c867e4b9166c767b804d46a59b5216cde7a4e99040c5a40433225ee282a1b0a06c523eaf4534d7f83fa1155b0047718cbc546a0d072b04b3564eea1b422273f548271a0bb2316053fa76991955ebd63159434ecebb4e466dae5a1073a6727627097a1049e617d91d361094fa68f0ff77987130305beaba2eda04df997b714d6c6f2c29a6ad5cb4022b02709b",
                "eead9d67890cbb22392336fea1851f38")
        };

        public static Rfc7539TestVector[] Rfc7634AeadTestVectors =
        {
            // Appendix A.
            new Rfc7539TestVector(
                "45000054a6f200004001e778c6336405c000020508005b7a3a080000553bec100007362708090a0b0c0d0e0f101112131415161718191a1b1c1d1e1f202122232425262728292a2b2c2d2e2f303132333435363701020204",
                "0102030400000005",
                "808182838485868788898a8b8c8d8e8f909192939495969798999a9b9c9d9e9f",
                "a0a1a2a31011121314151617",
                "24039428b97f417e3c13753a4f05087b67c352e6a7fab1b982d466ef407ae5c614ee8099d52844eb61aa95dfab4c02f72aa71e7c4c4f64c9befe2facc638e8f3cbec163fac469b502773f6fb94e664da9165b82829f641e0",
                "76aaa8266b7fb0f7b11b369907e1ad43"),
            // Appendix B.
            new Rfc7539TestVector(
                "0000000c000040010000000a00",
                "c0c1c2c3c4c5c6c7d0d1d2d3d4d5d6d72e202500000000090000004529000029",
                "808182838485868788898a8b8c8d8e8f909192939495969798999a9b9c9d9e9f",
                "a0a1a2a31011121314151617",
                "610394701f8d017f7c12924889",
                "6b71bfe25236efd7cdc67066906315b2")
        };
    }
}
