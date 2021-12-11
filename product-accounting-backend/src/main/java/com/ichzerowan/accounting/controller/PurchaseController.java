package com.ichzerowan.accounting.controller;

import com.ichzerowan.accounting.dao.*;
import com.ichzerowan.accounting.model.*;
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

    @GetMapping("")
    @Transactional
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
        return repository.save(newPurchase);
    }

    @PutMapping("/{id}")
    Purchase updatePurchase(@RequestBody PurchaseDto purchaseDto, @PathVariable Long id){
        return repository.findById(id).map(purchase -> {
            purchase.setDate(purchaseDto.getDate());
            return repository.save(purchase);
        }).orElseThrow(() -> new ObjectNotFoundException(Purchase.class, id));
    }

    @DeleteMapping("/{id}")
    void deletePurchase(@PathVariable Long id){
        Purchase purchase =  repository.findById(id)
                .orElseThrow(() -> new ObjectNotFoundException(Purchase.class, id));
        repository.delete(purchase);
    }

    @PostMapping("/{purchaseId}/products")
    Purchase addPurchaseProduct(@RequestBody PurchaseProductDto productDto, @PathVariable Long purchaseId){
        Purchase purchase =  repository.findById(purchaseId)
                .orElseThrow(() -> new ObjectNotFoundException(Purchase.class, purchaseId));
        Product product = productRepository.findById(productDto.getProductId())
                .orElseThrow(() -> new ObjectNotFoundException(Product.class, purchaseId));
        PurchaseProduct purchaseProduct = new PurchaseProduct(purchase, product, productDto.getCount());
        purchaseProductRepository.save(purchaseProduct);
        return purchase;
    }

    @PutMapping("/{purchaseId}/products/{productId}")
    Purchase updatePurchaseProduct(@RequestBody PurchaseProductDto productDto, @PathVariable Long purchaseId, @PathVariable Long productId){
        PurchaseProductId purchaseProductId = new PurchaseProductId(purchaseId, productId);
        PurchaseProduct purchaseProduct = purchaseProductRepository.findById(purchaseProductId)
                .orElseThrow(() -> new ObjectNotFoundException(PurchaseProduct.class, purchaseId));
        purchaseProduct.setCount(productDto.getCount());
        purchaseProductRepository.save(purchaseProduct);
        return purchaseProduct.getPurchase();
    }

    @DeleteMapping("/{purchaseId}/products/{productId}")
    void deletePurchaseProduct(@PathVariable Long purchaseId, @PathVariable Long productId){
        PurchaseProductId purchaseProductId = new PurchaseProductId(purchaseId, productId);
        PurchaseProduct purchaseProduct = purchaseProductRepository.findById(purchaseProductId)
                .orElseThrow(() -> new ObjectNotFoundException(PurchaseProduct.class, purchaseId));
        purchaseProductRepository.delete(purchaseProduct);
    }
}
