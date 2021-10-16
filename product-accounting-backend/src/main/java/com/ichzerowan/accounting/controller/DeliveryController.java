package com.ichzerowan.accounting.controller;

import com.ichzerowan.accounting.dao.*;
import com.ichzerowan.accounting.model.*;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.transaction.annotation.Transactional;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/deliveries")
public class DeliveryController {

    @Autowired
    private DeliveryRepository repository;

    @Autowired
    private ProductRepository productRepository;

    @Autowired
    private DeliveryProductRepository deliveryProductRepository;

    @Autowired
    private SupplierRepository supplierRepository;

    @GetMapping("")
    @Transactional
    List<Delivery> getAll(){
        return repository.findAll();
    }

    @GetMapping("/{id}")
    Delivery getDelivery(@PathVariable Long id){
        return repository.findById(id)
                .orElseThrow(() -> new ObjectNotFoundException(Delivery.class, id));
    }

    @PostMapping("")
    Delivery addDelivery(@RequestBody DeliveryDto deliveryDto){
        Supplier supplier = supplierRepository.findById(deliveryDto.getSupplierId())
                .orElseThrow(() -> new ObjectNotFoundException(Supplier.class, deliveryDto.getSupplierId()));
        Delivery newDelivery = new Delivery(deliveryDto.getDate(), supplier);
        return repository.save(newDelivery);
    }

    @PutMapping("/{id}")
    Delivery updateDelivery(@RequestBody DeliveryDto deliveryDto, @PathVariable Long id){
        return repository.findById(id).map(delivery -> {
            Supplier supplier = supplierRepository.findById(deliveryDto.getSupplierId())
                    .orElseThrow(() -> new ObjectNotFoundException(Supplier.class, deliveryDto.getSupplierId()));
            delivery.setDate(deliveryDto.getDate());
            delivery.setSupplier(supplier);
            return repository.save(delivery);
        }).orElseThrow(() -> new ObjectNotFoundException(Delivery.class, id));
    }

    @DeleteMapping("/{id}")
    void deleteDelivery(@PathVariable Long id){
        Delivery delivery =  repository.findById(id)
                .orElseThrow(() -> new ObjectNotFoundException(Delivery.class, id));
        repository.delete(delivery);
    }

    @PostMapping("/{deliveryId}/products")
    Delivery addDeliveryProduct(@RequestBody DeliveryProductDto productDto, @PathVariable Long deliveryId){
        Delivery delivery =  repository.findById(deliveryId)
                .orElseThrow(() -> new ObjectNotFoundException(Delivery.class, deliveryId));
        Product product = productRepository.findById(productDto.getProductId())
                .orElseThrow(() -> new ObjectNotFoundException(Product.class, deliveryId));
        DeliveryProduct deliveryProduct = new DeliveryProduct(delivery, product, productDto.getCount());
        deliveryProductRepository.save(deliveryProduct);
        return delivery;
    }

    @PutMapping("/{deliveryId}/products/{productId}")
    Delivery updateDeliveryProduct(@RequestBody DeliveryProductDto productDto, @PathVariable Long deliveryId, @PathVariable Long productId){
        DeliveryProductId deliveryProductId = new DeliveryProductId(deliveryId, productId);
        DeliveryProduct deliveryProduct = deliveryProductRepository.findById(deliveryProductId)
                .orElseThrow(() -> new ObjectNotFoundException(DeliveryProduct.class, deliveryId));
        deliveryProduct.setCount(productDto.getCount());
        deliveryProductRepository.save(deliveryProduct);
        return deliveryProduct.getDelivery();
    }

    @DeleteMapping("/{deliveryId}/products/{productId}")
    void deleteDeliveryProduct(@PathVariable Long deliveryId, @PathVariable Long productId){
        DeliveryProductId deliveryProductId = new DeliveryProductId(deliveryId, productId);
        DeliveryProduct deliveryProduct = deliveryProductRepository.findById(deliveryProductId)
                .orElseThrow(() -> new ObjectNotFoundException(DeliveryProduct.class, deliveryId));
        deliveryProductRepository.delete(deliveryProduct);
    }
}
