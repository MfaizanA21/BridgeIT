using BridgeIT.API.DTOs.PaymentDTOs;

namespace BridgeIT.API.services.Interface;

public interface IPaymentSlipService
{
    public Byte[] GeneratePaymentSlip(PaymentSlipDto paymentSlipDto);
}