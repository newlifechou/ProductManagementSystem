﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PMSWCFService.DataContracts;
using PMSWCFService.ServiceContracts;
using PMSDAL;
using PMSWCFService.ServiceImplements.Helpers;
using AutoMapper;

namespace PMSWCFService
{
    public class TCBService : ITCBService
    {
        public void AddDeliveryItemTCB(DcDeliveryItem model)
        {
            try
            {
                XS.RunLog();
                using (var db = new PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<DcDeliveryItem, DeliveryItem>());
                    var entity = Mapper.Map<DeliveryItem>(model);
                    db.DeliveryItems.Add(entity);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public List<DcDelivery> GetDelivery(int s, int t, string deliveryname)
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDbContext())
                {
                    var query = from d in dc.Deliverys
                                where d.State != PMSCommon.DeliveryState.作废.ToString()
                                && d.DeliveryName.Contains(deliveryname)
                                && d.Receiver.Contains("TCB")
                                orderby d.CreateTime descending
                                select d;
                    Mapper.Initialize(cfg =>
                    {
                        cfg.CreateMap<Delivery, DcDelivery>();
                    });

                    var records = Mapper.Map<List<Delivery>, List<DcDelivery>>(query.Skip(s).Take(t).ToList());
                    return records;
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public int GetDeliveryCount(string deliveryname)
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDbContext())
                {
                    var query = from d in dc.Deliverys
                                where d.State != PMSCommon.DeliveryState.作废.ToString()
                                && d.DeliveryName.Contains(deliveryname)
                                && d.Receiver.Contains("TCB")
                                select d;
                    return query.Count();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public List<DcDeliveryItem> GetDeliveryItemTCBByDeliveryID(Guid id)
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<DeliveryItem, DcDeliveryItem>());

                    var result = dc.DeliveryItems
                        .Where(i => i.DeliveryID == id && i.State != PMSCommon.SimpleState.作废.ToString())
                        .OrderByDescending(i => i.CreateTime)
                        .ToList();
                    return Mapper.Map<List<DeliveryItem>, List<DcDeliveryItem>>(result);
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public List<DcDeliveryItem> GetDeliveryItemTCB(int s, int t, string productid, string composition, string po,
            string customer, string bondingpo, string state)
        {
            try
            {
                XS.RunLog();
                var searchItem = CompositionHelper.GetSearchItems(composition);
                using (var dc = new PMSDbContext())
                {
                    var query = from d in dc.DeliveryItems
                                where d.State != PMSCommon.SimpleState.作废.ToString()
                                && d.ProductID.Contains(productid)
                                && d.PO.Contains(po)
                                && d.Customer.Contains(customer)
                                && d.BondingPO.Contains(bondingpo)
                                && d.TCBState.Contains(state)
                                && d.Composition.Contains(searchItem.Item1)
                                && d.Composition.Contains(searchItem.Item2)
                                && d.Composition.Contains(searchItem.Item3)
                                && d.Composition.Contains(searchItem.Item4)
                                orderby d.CreateTime descending
                                select d;
                    Mapper.Initialize(cfg =>
                    {
                        cfg.CreateMap<DeliveryItem, DcDeliveryItem>();
                    });

                    var records = Mapper.Map<List<DeliveryItem>, List<DcDeliveryItem>>(query.Skip(s).Take(t).ToList());
                    return records;
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public int GetDeliveryItemTCBCount(string productid, string composition, string po, string customer, string bondingpo, string state)
        {
            try
            {
                XS.RunLog();
                var searchItem = CompositionHelper.GetSearchItems(composition);
                using (var dc = new PMSDbContext())
                {
                    var query = from d in dc.DeliveryItems
                                where d.State != PMSCommon.SimpleState.作废.ToString()
                                && d.ProductID.Contains(productid)
                                && d.PO.Contains(po)
                                && d.Customer.Contains(customer)
                                && d.BondingPO.Contains(bondingpo)
                                && d.TCBState.Contains(state)
                                && d.Composition.Contains(searchItem.Item1)
                                && d.Composition.Contains(searchItem.Item2)
                                && d.Composition.Contains(searchItem.Item3)
                                && d.Composition.Contains(searchItem.Item4)
                                select d;

                    return query.Count();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public void UpdateDeliveryItemTCB(DcDeliveryItem model)
        {
            try
            {
                XS.RunLog();

                using (var dc = new PMSDbContext())
                {
                    var config = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<DcDeliveryItem, DeliveryItem>();
                    });
                    var mapper = config.CreateMapper();
                    var entity = mapper.Map<DeliveryItem>(model);
                    dc.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                    dc.SaveChanges();

                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public List<DcDelivery> GetDeliveryUnFinished()
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDbContext())
                {
                    var query = from d in dc.Deliverys
                                where d.State == PMSCommon.DeliveryState.未完成.ToString()
                                && d.Receiver.Contains("TCB")
                                orderby d.CreateTime descending
                                select d;
                    Mapper.Initialize(cfg =>
                    {
                        cfg.CreateMap<Delivery, DcDelivery>();
                    });

                    var records = Mapper.Map<List<Delivery>, List<DcDelivery>>(query.ToList());
                    return records;
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public List<DcDeliveryItemExtra> GetDeliveryItemExtra(int s, int t, string productid, string composition, string po,
            string customer, string bondingpo, string state)
        {
            try
            {
                XS.RunLog();
                var searchItem = CompositionHelper.GetSearchItems(composition);
                using (var dc = new PMSDbContext())
                {
                    var query = from d in dc.DeliveryItems
                                join dd in dc.Deliverys
                                on d.DeliveryID equals dd.ID
                                where d.State != PMSCommon.SimpleState.作废.ToString()
                                && dd.State != PMSCommon.DeliveryState.作废.ToString()
                                && d.ProductID.Contains(productid)
                                && d.PO.Contains(po)
                                && d.Customer.Contains(customer)
                                && d.BondingPO.Contains(bondingpo)
                                && d.TCBState.Contains(state)
                                && d.Composition.Contains(searchItem.Item1)
                                && d.Composition.Contains(searchItem.Item2)
                                && d.Composition.Contains(searchItem.Item3)
                                && d.Composition.Contains(searchItem.Item4)
                                && dd.Receiver.Contains("TCB")
                                orderby dd.CreateTime descending, d.CreateTime descending
                                select new PMSDeliveryItemExtra()
                                {
                                    Delivery = dd,
                                    DeliveryItem = d
                                };

                    Mapper.Initialize(cfg =>
                    {
                        cfg.CreateMap<PMSDeliveryItemExtra, DcDeliveryItemExtra>();
                        cfg.CreateMap<Delivery, DcDelivery>();
                        cfg.CreateMap<DeliveryItem, DcDeliveryItem>();
                    });

                    var records = Mapper.Map<List<PMSDeliveryItemExtra>, List<DcDeliveryItemExtra>>(query.Skip(s).Take(t).ToList());
                    return records;
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public int GetDeliveryItemExtraCount(string productid, string composition, string po, string customer, 
            string bondingpo, string state)
        {
            try
            {
                XS.RunLog();
                var searchItem = CompositionHelper.GetSearchItems(composition);
                using (var dc = new PMSDbContext())
                {
                    var query = from d in dc.DeliveryItems
                                join dd in dc.Deliverys
                                on d.DeliveryID equals dd.ID
                                where d.State != PMSCommon.SimpleState.作废.ToString()
                                && dd.State != PMSCommon.DeliveryState.作废.ToString()
                                && d.ProductID.Contains(productid)
                                && d.PO.Contains(po)
                                && d.Customer.Contains(customer)
                                && d.BondingPO.Contains(bondingpo)
                                && d.TCBState.Contains(state)
                                && d.Composition.Contains(searchItem.Item1)
                                && d.Composition.Contains(searchItem.Item2)
                                && d.Composition.Contains(searchItem.Item3)
                                && d.Composition.Contains(searchItem.Item4)
                                && dd.Receiver.Contains("TCB")
                                select new PMSDeliveryItemExtra()
                                {
                                    Delivery = dd,
                                    DeliveryItem = d
                                };
                    return query.Count();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }
    }
}