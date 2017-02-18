﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PMSWCFService.DataContracts;
using PMSWCFService.ServiceContracts;
using AutoMapper;

namespace PMSWCFService
{
    public partial class PMSService : IRecordDeliveryService
    {
        public int AddRecordDelivery(DcRecordDelivery model)
        {
            using (var dc = new PMSDAL.PMSDbContext())
            {
                int result = 0;
                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<DcRecordDelivery, PMSDAL.RecordDelivery>();
                    cfg.CreateMap<DcRecordDeliveryItem, PMSDAL.RecordDeliveryItem>();
                });
                var record = Mapper.Map<PMSDAL.RecordDelivery>(model);
                dc.RecordDeliverys.Add(record);
                result = dc.SaveChanges();
                return result;
            }
        }

        public int AddRecordDeliveryItem(DcRecordDeliveryItem model)
        {
            using (var dc = new PMSDAL.PMSDbContext())
            {
                int result = 0;
                Mapper.Initialize(cfg => cfg.CreateMap<DcRecordDeliveryItem, PMSDAL.RecordDeliveryItem>());
                var record = Mapper.Map<PMSDAL.RecordDeliveryItem>(model);
                dc.RecordDeliveryItems.Add(record);
                result = dc.SaveChanges();
                return result;
            }
        }

        public int DeleteRecordDelivery(Guid id)
        {
            using (var dc = new PMSDAL.PMSDbContext())
            {
                int result = 0;
                var record = dc.RecordDeliverys.Find(id);
                dc.RecordDeliverys.Remove(record);
                result = dc.SaveChanges();
                return result;
            }
        }

        public int DeleteRecordDeliveryItem(Guid id)
        {
            using (var dc = new PMSDAL.PMSDbContext())
            {
                int result = 0;
                var record = dc.RecordDeliveryItems.Find(id);
                dc.RecordDeliveryItems.Remove(record);
                result = dc.SaveChanges();
                return result;
            }
        }

        public List<DcRecordDelivery> GetDelivery(int skip, int take)
        {
            using (var dc = new PMSDAL.PMSDbContext())
            {
                var result = dc.RecordDeliverys.Include("DeliveryItems")
                    .OrderByDescending(d => d.CreateTime)
                    .ToList();
                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<PMSDAL.RecordDelivery, DcRecordDelivery>();
                    cfg.CreateMap<PMSDAL.RecordDeliveryItem, DcRecordDeliveryItem>();
                });

                var records = Mapper.Map<List<PMSDAL.RecordDelivery>, List<DcRecordDelivery>>(result);

                return records;
            }
        }

        public int GetDeliveryCount()
        {
            using (var dc = new PMSDAL.PMSDbContext())
            {
                return dc.RecordDeliverys.Count();
            }
        }

        public int UpdateReocrdDelivery(DcRecordDelivery model)
        {
            using (var dc = new PMSDAL.PMSDbContext())
            {
                int result = 0;
                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<DcRecordDelivery, PMSDAL.RecordDelivery>();
                    cfg.CreateMap<DcRecordDeliveryItem, PMSDAL.RecordDeliveryItem>();
                });
                var record = Mapper.Map<PMSDAL.RecordDelivery>(model);
                dc.Entry(record).State = System.Data.Entity.EntityState.Modified;
                result = dc.SaveChanges();
                return result;
            }
        }

        public int UpdateReocrdDeliveryItem(DcRecordDeliveryItem model)
        {
            using (var dc = new PMSDAL.PMSDbContext())
            {
                int result = 0;
                Mapper.Initialize(cfg => cfg.CreateMap<DcRecordDeliveryItem, PMSDAL.RecordDeliveryItem>());
                var record = Mapper.Map<PMSDAL.RecordDeliveryItem>(model);
                dc.Entry(record).State = System.Data.Entity.EntityState.Modified;
                result = dc.SaveChanges();
                return result;
            }
        }
    }
}