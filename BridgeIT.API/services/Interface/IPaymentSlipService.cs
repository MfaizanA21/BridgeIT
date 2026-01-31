using BridgeITAPIs.DTOs.PaymentDTOs;

namespace BridgeITAPIs.services.Interface;

public interface IPaymentSlipService
{
    public Byte[] GeneratePaymentSlip(PaymentSlipDto paymentSlipDto);
}