package com.ichzerowan.accounting.controller;

import com.ichzerowan.accounting.dao.CouponRepository;
import com.ichzerowan.accounting.dao.ObjectNotFoundException;
import com.ichzerowan.accounting.model.coupon.Coupon;
import com.ichzerowan.accounting.model.coupon.CouponCountDto;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/coupons")
public class CouponController {

    @Autowired
    private CouponRepository repository;

    @GetMapping("")
    List<Coupon> getAll(){
        return repository.findByArchived(false);
    }

    @GetMapping("/archived")
    List<Coupon> getArchived(){
        return repository.findByArchived(true);
    }

    @GetMapping("/{id}")
    Coupon getCoupon(@PathVariable Long id){
        return repository.findById(id)
                .orElseThrow(() -> new ObjectNotFoundException(Coupon.class, id));
    }

    @PostMapping("")
    Coupon addCoupon(@RequestBody Coupon coupon){
        return repository.save(coupon);
    }

    @PutMapping("/{id}/add")
    Coupon updateCoupon(@RequestBody CouponCountDto couponCount, @PathVariable Long id){
        return repository.findById(id).map(coupon -> {
            coupon.setCount(coupon.getCount() + couponCount.getCount());
            return repository.save(coupon);
        }).orElseThrow(() -> new ObjectNotFoundException(Coupon.class, id));
    }

    @DeleteMapping("/{id}")
    Coupon deleteCoupon(@PathVariable Long id){
        return repository.findById(id).map(coupon -> {
            coupon.setArchived(true);
            return repository.save(coupon);
        }).orElseThrow(() -> new ObjectNotFoundException(Coupon.class, id));
    }
}
