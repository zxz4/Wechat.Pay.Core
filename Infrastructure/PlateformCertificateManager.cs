using System.Collections.Concurrent;
using System.Security.Cryptography.X509Certificates;

namespace Wechat.Pay.Core.Infrastructure
{
    /// <summary>
    /// 统一管理平台证书
    /// </summary>
    internal class PlateformCertificateManager
    {

        private readonly ConcurrentDictionary<string, X509Certificate2> certificate = new ConcurrentDictionary<string, X509Certificate2>();

        private static readonly PlateformCertificateManager certificateManager = new PlateformCertificateManager();

        private PlateformCertificateManager()
        {
        }


        internal static PlateformCertificateManager CertificateManager
        {
            get
            {
                return certificateManager;
            }
        }

        internal X509Certificate2 this[string serialNo]
        {
            get
            {
                if (certificate.TryGetValue(serialNo, out X509Certificate2 value))

                    return value;

                return null;
            }
            set
            {
                certificate.TryAdd(serialNo, value);
            }
        }
    }
}
