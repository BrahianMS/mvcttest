package com.mvct.demo.listener;

import org.springframework.amqp.rabbit.annotation.RabbitListener;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.mvct.demo.config.RabbitMQConfig;
import com.mvct.demo.dto.TestPingMessage;
import com.mvct.demo.service.ConnectivityLogService;

@Service
public class TestPingListener {

    @Autowired
    private ConnectivityLogService service;

    @RabbitListener(queues = RabbitMQConfig.TEST_QUEUE)
    public void onMessage(TestPingMessage msg) {
        System.out.println("📩 Mensaje recibido: " + msg);
        service.save(msg);
    }
}
