package com.ichzerowan.accounting.dao;

public class OutOfCouponsException extends RuntimeException {

    public OutOfCouponsException(long couponId){
        super(String.format("Unable to apply coupon %d, no more coupons left", couponId));
    }
}
