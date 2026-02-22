using BridgeIT.API.DTOs.PaymentDTOs;

namespace BridgeIT.Application.Abstractions.Service.Interface;

public interface IPaymentSlipService
{
    public Byte[] GeneratePaymentSlip(PaymentSlipDto paymentSlipDto);
}