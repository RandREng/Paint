using System;
using System.Collections.Generic;
using System.Text;

namespace Paint.Domain
{
    public interface IPaintRepository
    {
        public T GetBidSheet<T>(int i);
        public BidSheet GetBidSheet(int i);
    }
}
