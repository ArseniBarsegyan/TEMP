﻿using System.Collections.Generic;
using UserStore.BLL.DTO;
using UserStore.BLL.Infrastructure;

namespace UserStore.BLL.Interfaces
{
    public interface IPurchaseService
    {
        IEnumerable<PurchaseDto> GetAllSalesList();
        PurchaseDto GetPurchaseDtoById(int id);
        OperationDetails Create(PurchaseDto purchaseDto);
        OperationDetails Edit(PurchaseDto purchaseDto);
        OperationDetails Delete(int id);
    }
}