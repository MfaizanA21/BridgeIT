using BridgeIT.API.DTOs.PaymentDTOs;

namespace BridgeIT.Application.Services.Interface;

public interface IPaymentSlipService
{
    public Byte[] GeneratePaymentSlip(PaymentSlipDto paymentSlipDto);
}