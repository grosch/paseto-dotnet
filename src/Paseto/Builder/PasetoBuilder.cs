﻿namespace Paseto.Builder
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Algorithms;
    using Extensions;
    using Protocol;
    using Serializers;
    using Utils;
    using static Utils.EncodingHelper;

    /// <summary>
    /// Build and decode a Paseto with Fluent API.
    /// </summary>
    public sealed class PasetoBuilder<TProtocol> where TProtocol : IPasetoProtocol, new()
    {
        private readonly PasetoData _paseto = new PasetoData();

        private IJsonSerializer _serializer = new JsonNetSerializer();
        //private IBase64UrlEncoder _urlEncoder = new Base64UrlEncoder();

        private string _key;
        private string _footer;
        private Purpose _purpose;
        private bool _verify;

        /// <summary>
        /// Sets the base64 secret key (for encoding) or the base64 public key (for decoding and validating) to the Paseto.
        /// </summary>
        /// <returns>Current builder instance</returns>
        public PasetoBuilder<TProtocol> WithKey(string key)
        {
            _key = key;
            return this;
        }

        /// <summary>
        /// Adds claim to the Paseto.
        /// </summary>
        /// <param name="name">Claim name</param>
        /// <param name="value">Claim value</param>
        /// <returns>Current builder instance</returns>
        public PasetoBuilder<TProtocol> AddClaim(string name, object value)
        {
            _paseto.Payload.Add(name, value);
            return this;
        }

        /// <summary>
        /// Add string claim to the Paseto.
        /// </summary>
        /// <param name="name">Claim name</param>
        /// <param name="value">Claim value</param>
        /// <returns>Current builder instance</returns>
        public PasetoBuilder<TProtocol> AddClaim(string name, string value) => AddClaim(name, (object)value);

        /// <summary>
        /// Adds well-known claim to the Paseto.
        /// </summary>
        /// <param name="name">Well-known registered claim name</param>
        /// <param name="value">Claim value</param>
        /// <returns>Current builder instance</returns>
        public PasetoBuilder<TProtocol> AddClaim(RegisteredClaims name, string value) => AddClaim(name.GetRegisteredClaimName(), value);

        /// <summary>
        /// Adds the footer to the Paseto.
        /// </summary>
        /// <param name="footer">The footer.</param>
        /// <returns>PasetoBuilder&lt;TProtocol&gt;.</returns>
        public PasetoBuilder<TProtocol> AddFooter(string footer)
        {
            _footer = footer;
            return this;
        }

        /*
        public PasetoBuilder<TProtocol> WithPurpose(Purpose purpose)
        {
            _purpose = purpose;
            return this;
        }
        */

        /// <summary>
        /// Sets the Paseto's purpose as public.
        /// </summary>
        /// <returns>Current builder instance</returns>
        public PasetoBuilder<TProtocol> AsPublic()
        {
            _purpose = Purpose.Public;
            return this;
        }

        /// <summary>
        /// Sets the Paseto's purpose as local.
        /// </summary>
        /// <returns>Current builder instance</returns>
        public PasetoBuilder<TProtocol> AsLocal()
        {
            _purpose = Purpose.Local;
            return this;
        }

        /// <summary>
        /// Instructs if it should verify the signature when decoding.
        /// </summary>
        /// <param name="verify">if set to <c>true</c> it will verify the signature when decoding.</param>
        /// <returns>Current builder instance</returns>
        public PasetoBuilder<TProtocol> AndVerifySignature(bool verify = true)
        {
            _verify = verify;
            return this;
        }

        /// <summary>
        /// Builds a token using the supplied dependencies.
        /// </summary>
        /// <returns>The generated Paseto.</returns>
        /// <exception cref="InvalidOperationException">Thrown if either algorithm, serializer, encoder or secret is null.</exception>
        public string Build()
        {
            if (string.IsNullOrEmpty(_key))
                throw new InvalidOperationException("Can't build a token. Check if you have call the 'WithSecret' method.");

            if (_paseto.Payload is null || _paseto.Payload.Count == 0)
                throw new InvalidOperationException("Can't build a token. Check if you have call the 'AddClaim' method.");

            var proto = new TProtocol();
            var payload = _serializer.Serialize(_paseto.Payload);

            switch (_purpose)
            {
                case Purpose.Local:
                    throw new NotSupportedException("The Local Purpose is not currently supported!");
                case Purpose.Public:
                    return proto.Sign(Convert.FromBase64String(_key), payload, _footer ?? string.Empty);
                default:
                    throw new NotImplementedException($"The Purpose {_purpose} is not defined!");
            }
        }

        /// <summary>
        /// Decodes a token using the supplied dependencies.
        /// </summary>
        /// <param name="token">The Paseto token.</param>
        /// <returns>The JSON payload</returns>
        public string Decode(string token)
        {
            if (string.IsNullOrEmpty(_key))
                throw new InvalidOperationException("Can't build a token. Check if you have call the 'WithSecret' method.");

            var proto = new TProtocol();

            switch (_purpose)
            {
                case Purpose.Local:
                    throw new NotSupportedException("The Local Purpose is not currently supported!");
                case Purpose.Public:
                    if (!_verify)
                        return proto.Verify(token, Convert.FromBase64String(_key)).Payload;

                    var (valid, payload) = proto.Verify(token, Convert.FromBase64String(_key));
                    if (!valid)
                        throw new SignatureVerificationException("Invalid signature!");

                    return payload;
                default:
                    throw new NotImplementedException($"The Purpose {_purpose} is not defined!");
            }
        }
    }
}