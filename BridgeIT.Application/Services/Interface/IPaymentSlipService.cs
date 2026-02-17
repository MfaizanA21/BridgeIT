using BridgeIT.API.DTOs.PaymentDTOs;

namespace BridgeIT.Application.Interface;

public interface IPaymentSlipService
{
    public Byte[] GeneratePaymentSlip(PaymentSlipDto paymentSlipDto);
}