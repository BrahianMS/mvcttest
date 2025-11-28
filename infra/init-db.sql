CREATE TABLE IF NOT EXISTS connectivity_log (
    event_id UUID PRIMARY KEY,
    status VARCHAR(50) DEFAULT 'PENDING',
    csharp_timestamp TIMESTAMP WITH TIME ZONE,
    java_timestamp TIMESTAMP WITH TIME ZONE,
    event_type VARCHAR(100),
    source_ip VARCHAR(45),
    payload JSONB,
    created_at TIMESTAMP WITH TIME ZONE DEFAULT NOW()
);