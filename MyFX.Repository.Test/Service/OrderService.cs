﻿/****************************************************************************************
 * 文件名：OrderService
 * 作者：huangzl
 * 创始时间：2018/1/18 17:32:19
 * 创建说明：
****************************************************************************************/

using MyFX.Core.BaseModel.Paging;
using MyFX.Core.Repository;
using MyFX.Repository.Test.DAL;
using MyFX.Repository.Test.Domain;
using MyFX.Repository.Test.Dtos.Request;
using MyFX.Repository.Test.Dtos.Response;

namespace MyFX.Repository.Test.Service
{
    public class OrderService : IOrderService
    {
        private IOrderRepository _orderRepository { get;set; }
        private IUnitOfWork _uow { get; set; }

        public OrderService(IOrderRepository orderRepository, IUnitOfWorkFactory uowFactory)
        {
            _orderRepository = orderRepository;
            _uow = uowFactory.Create();
        }

        public void CreateOrder(Order order)
        {
            _orderRepository.Add(order);
            order.StoreId = 7878789;
            _uow.Commit();
        }

        public GetOrderResult GetOrder(GetOrderRequest request)
        {
            var order = _orderRepository.First(o => o.OrderNo.Equals(request.OrderNo));
            GetOrderResult rs = new GetOrderResult();
            rs.retBody = order;

            return rs;
        }

        public FindOrdersResult FindOrders(FindOrdersRequest request)
        {
            var pagedQuery = new PagedQuery(request.PageIndex, request.PageSize);
            var pageOrders = _orderRepository.FindPageList(pagedQuery, null, o => o.OrderNo, false);
            FindOrdersResult rs = new FindOrdersResult();
            rs.retBody = pageOrders;

            return rs;
        }
    }
}
