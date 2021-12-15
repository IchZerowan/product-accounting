package com.ichzerowan.accounting.controller;

import com.ichzerowan.accounting.dao.*;
import com.ichzerowan.accounting.model.coupon.Coupon;
import com.ichzerowan.accounting.model.delivery.Delivery;
import com.ichzerowan.accounting.model.product.Product;
import com.ichzerowan.accounting.model.purchase.*;
import com.ichzerowan.accounting.model.transaction.Transaction;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.transaction.annotation.Transactional;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/purchases")
public class PurchaseController {

    @Autowired
    private PurchaseRepository repository;

    @Autowired
    private ProductRepository productRepository;

    @Autowired
    private PurchaseProductRepository purchaseProductRepository;

    @Autowired
    private SupplierRepository supplierRepository;

    @Autowired
    private TransactionRepository transactionRepository;

    @Autowired
    private CouponRepository couponRepository;

    @GetMapping("")
    List<Purchase> getAll(){
        return repository.findAll();
    }

    @GetMapping("/{id}")
    Purchase getPurchase(@PathVariable Long id){
        return repository.findById(id)
                .orElseThrow(() -> new ObjectNotFoundException(Purchase.class, id));
    }

    @PostMapping("")
    Purchase addPurchase(@RequestBody PurchaseDto purchaseDto){
        Purchase newPurchase = new Purchase(purchaseDto.getDate());
        Coupon coupon = couponRepository.findFirstByCode(purchaseDto.getCouponCode());
        newPurchase.setCoupon(coupon);
        newPurchase.updateTotal();
        return repository.save(newPurchase);
    }

    @PutMapping("/{id}")
    Purchase updatePurchase(@RequestBody PurchaseDto purchaseDto, @PathVariable Long id){
        return repository.findById(id).map(purchase -> {
            if(purchase.isCompleted())
                throw new ModificationNotAllowedException(Purchase.class, purchase.getId());
            Coupon coupon = couponRepository.findFirstByCode(purchaseDto.getCouponCode());
            purchase.setCoupon(coupon);
            purchase.setDate(purchaseDto.getDate());
            purchase.updateTotal();
            return repository.save(purchase);
        }).orElseThrow(() -> new ObjectNotFoundException(Purchase.class, id));
    }

    @Transactional
    @PutMapping("/{id}/commit")
    Purchase commitPurchase(@PathVariable Long id){
        return repository.findById(id).map(purchase -> {
            if(purchase.isCompleted())
                throw new ModificationNotAllowedException(Purchase.class, purchase.getId());

            if(purchase.getProducts().size() == 0)
                throw new EmptyTransactionException(Purchase.class, purchase.getId());

            for(PurchaseProduct purchaseProduct: purchase.getProducts()) {
                Product product = purchaseProduct.getProduct();
                if(product.getCount() < purchaseProduct.getCount()){
                    throw new OutOfStockException(product.getId(), purchaseProduct.getCount(), product.getCount());
                }
                product.setCount(product.getCount() - purchaseProduct.getCount());
                productRepository.save(product);
            }

            Coupon coupon = purchase.getCoupon();
            if(coupon != null){
                coupon.setCount(coupon.getCount() - 1);
                couponRepository.save(coupon);
            }

            Transaction transaction = new Transaction(Transaction.Type.PURCHASE, purchase.getId(), purchase.getTotal());
            transactionRepository.save(transaction);
            purchase.setCompleted(true);

            return repository.save(purchase);
        }).orElseThrow(() -> new ObjectNotFoundException(Purchase.class, id));
    }

    @DeleteMapping("/{id}")
    void deletePurchase(@PathVariable Long id){
        Purchase purchase =  repository.findById(id)
                .orElseThrow(() -> new ObjectNotFoundException(Purchase.class, id));
        if(purchase.isCompleted())
            throw new ModificationNotAllowedException(Purchase.class, purchase.getId());
        repository.delete(purchase);
    }

    @PostMapping("/{purchaseId}/products")
    Purchase addPurchaseProduct(@RequestBody PurchaseProductDto productDto, @PathVariable Long purchaseId){
        Purchase purchase =  repository.findById(purchaseId)
                .orElseThrow(() -> new ObjectNotFoundException(Purchase.class, purchaseId));
        if(purchase.isCompleted())
            throw new ModificationNotAllowedException(Purchase.class, purchase.getId());
        Product product = productRepository.findById(productDto.getProductId())
                .orElseThrow(() -> new ObjectNotFoundException(Product.class, productDto.getProductId()));
        PurchaseProduct purchaseProduct = new PurchaseProduct(purchase, product, productDto.getCount());
        purchaseProductRepository.save(purchaseProduct);
        purchase.updateTotal();
        repository.save(purchase);
        return purchase;
    }

    @PutMapping("/{purchaseId}/products/{productId}")
    Purchase updatePurchaseProduct(@RequestBody PurchaseProductDto productDto, @PathVariable Long purchaseId, @PathVariable Long productId){
        PurchaseProductId purchaseProductId = new PurchaseProductId(purchaseId, productId);
        PurchaseProduct purchaseProduct = purchaseProductRepository.findById(purchaseProductId)
                .orElseThrow(() -> new ObjectNotFoundException(PurchaseProduct.class, productId));
        Purchase purchase = purchaseProduct.getPurchase();
        if(purchase.isCompleted())
            throw new ModificationNotAllowedException(Purchase.class, purchase.getId());
        purchaseProduct.setCount(productDto.getCount());
        purchaseProductRepository.save(purchaseProduct);
        purchase.updateTotal();
        repository.save(purchase);
        return purchase;
    }

    @DeleteMapping("/{purchaseId}/products/{productId}")
    Purchase deletePurchaseProduct(@PathVariable Long purchaseId, @PathVariable Long productId){
        PurchaseProductId purchaseProductId = new PurchaseProductId(purchaseId, productId);
        PurchaseProduct purchaseProduct = purchaseProductRepository.findById(purchaseProductId)
                .orElseThrow(() -> new ObjectNotFoundException(PurchaseProduct.class, purchaseId));
        Purchase purchase = purchaseProduct.getPurchase();
        if(purchase.isCompleted())
            throw new ModificationNotAllowedException(Purchase.class, purchase.getId());
        purchaseProductRepository.delete(purchaseProduct);
        purchase.updateTotal();
        repository.save(purchase);
        return purchase;
    }
}
